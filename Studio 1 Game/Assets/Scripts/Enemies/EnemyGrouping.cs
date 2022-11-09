using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrouping : Enemy
{
    private GroupingStates stateCurrent = GroupingStates.Idling;

    public float aggroRange = 8f;
    public float locationPadding = 1.2f;
    private float bobbingDistance = 2.2f;

    private bool playerHit = false;
    public int groupMember = 0;

    public AudioSource hitSource;

    private ParticleSystem ps;

    public enum GroupingStates
    {
        Idling = 0,
        Grouping,
        Bobbing,
        Dead
    }

    protected override void Start()
    {
        base.Start();
        ps = GetComponentInChildren<ParticleSystem>();
        maxHealth = 30f;
        currentHealth = maxHealth;
        StateIdlingEnter();
    }

    void Update()
    {
        if (stateCurrent == GroupingStates.Bobbing)
        {
            switch (groupMember)
            {
                case 0:
                    base.agent.destination = player.transform.position + new Vector3(bobbingDistance, 0f, 0f);
                    break;

                case 1:
                    base.agent.destination = player.transform.position + new Vector3(bobbingDistance, 0f, -bobbingDistance);
                    break;

                case 2:
                    base.agent.destination = player.transform.position + new Vector3(0f, 0f, -bobbingDistance);
                    break;

                case 3:
                    base.agent.destination = player.transform.position + new Vector3(-bobbingDistance, 0f, -bobbingDistance);
                    break;

                case 4:
                    base.agent.destination = player.transform.position + new Vector3(-bobbingDistance, 0f, 0f);
                    break;

                case 5:
                    base.agent.destination = player.transform.position + new Vector3(-bobbingDistance, 0f, bobbingDistance);
                    break;

                case 6:
                    base.agent.destination = player.transform.position + new Vector3(0f, 0f, bobbingDistance);
                    break;

                case 7:
                    base.agent.destination = player.transform.position + new Vector3(bobbingDistance, 0f, bobbingDistance);
                    break;
            }
        }
        else if (stateCurrent == GroupingStates.Grouping)
        {
            base.agent.destination = player.position;
        }
    }

    public override void FSMProcess()
    {
        base.playerDistance = Vector3.Distance(transform.position, player.position);
        switch (stateCurrent)
        {
            case GroupingStates.Idling:
                if (isDead) { StateIdlingExit(); StateDeadEnter(); }
                else if (playerDistance <= aggroRange) { StateIdlingExit(); StateBobbingEnter(); }
                else { StateIdlingRemain(); }
                break;

            case GroupingStates.Grouping:
                if (isDead) { StateGroupingExit(); StateDeadEnter(); }
                else if (Vector3.Distance(transform.position, base.agent.destination) <= locationPadding) { StateGroupingExit(); StateBobbingEnter(); }
                else { StateGroupingRemain(); }
                break;

            case GroupingStates.Bobbing:
                if (isDead) { StateBobbingExit(); StateDeadEnter(); }
                else if (Vector3.Distance(transform.position, base.agent.destination) <= locationPadding) { StateBobbingExit(); StateGroupingEnter(); }
                else { StateBobbingRemain(); }
                break;

            case GroupingStates.Dead:
                StateDeadRemain();
                break;
        }

        if (base.agent.destination.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void StateIdlingEnter()
    {
        stateCurrent = GroupingStates.Idling;
        base.agent.destination = transform.position;
    }

    void StateIdlingRemain()
    {
        
    }

    void StateIdlingExit()
    {

    }

    void StateGroupingEnter()
    {
        base.animator.SetBool("playerDetected", true);
        stateCurrent = GroupingStates.Grouping;

        playerHit = false;
    }

    void StateGroupingRemain()
    {
        
    }

    void StateGroupingExit()
    {

    }

    void StateBobbingEnter()
    {
        stateCurrent = GroupingStates.Bobbing;
    }

    void StateBobbingRemain()
    {

    }

    void StateBobbingExit()
    {
        
    }

    void StateDeadEnter()
    {
        stateCurrent = GroupingStates.Dead;
        base.agent.destination = transform.position;
        Destroy(gameObject);
    }

    void StateDeadRemain()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && stateCurrent == GroupingStates.Grouping)
        {
            col.gameObject.SendMessage("ChangeHealth", -1f);
            playerHit = true;
            hitSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && stateCurrent == GroupingStates.Grouping)
        {
            other.gameObject.SendMessage("ChangeHealth", -1f);
            hitSource.Play();
        }
    }

    public override void ChangeHealth(float amount)
    {
        base.ChangeHealth(amount);
        if (amount < 0)
        {
            ps.Play();
        }
    }
}
