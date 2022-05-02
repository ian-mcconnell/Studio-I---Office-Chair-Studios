using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHedge : Enemy
{
    public Transform[] waypoints;
    private int waypointItterator = 0;
    Transform targetWaypoint;
    private float bufferDistance = 6f;

    private HedgeStates stateCurrent = HedgeStates.Idling;

    public GameObject whirlAttack;
    public GameObject basketballObj;
    public Transform[] ballSpawns;

    private int attackCooldown = 6;
    private int whirlCount = 0;

    private ParticleSystem ps;
    private SpriteRenderer sr;

    public enum HedgeStates
    {
        Idling = 0,
        Whirling,
        Hooping,
        Dead
    }

    protected override void Start()
    {
        targetWaypoint = waypoints[waypointItterator % waypoints.Length];
        sr = GetComponent<SpriteRenderer>();
        base.Start();
        ps = GetComponentInChildren<ParticleSystem>();
        StateIdlingEnter();
    }

    public override void FSMProcess()
    {
        base.playerDistance = Vector3.Distance(transform.position, player.position);
        attackCooldown -= 1;
        if (attackCooldown < 0)
        {
            attackCooldown = 0;
        }

        if (stateCurrent != HedgeStates.Whirling)
        {
            if (transform.position.x < transform.position.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }

        switch (stateCurrent)
        {
            case HedgeStates.Idling:
                if (isDead) { StateIdlingExit(); StateDeadEnter(); }
                else if (whirlCount >= 3 && attackCooldown == 0) { StateIdlingExit(); StateHoopingEnter(); }
                else if (whirlCount == 0 && attackCooldown == 0) { StateIdlingExit(); StateWhirlingEnter(); }
                else { StateIdlingRemain(); }
                break;

            case HedgeStates.Whirling:
                if (isDead) { StateWhirlingExit(); StateDeadEnter(); }
                else if (whirlCount >= 3) { StateWhirlingExit(); StateIdlingEnter(); }
                else { StateWhirlingRemain(); }
                break;

            case HedgeStates.Hooping:
                if (isDead) { StateHoopingExit(); StateDeadEnter(); }
                else if (whirlCount == 0) { StateHoopingExit(); StateIdlingEnter(); }
                else { StateHoopingRemain(); }
                break;

            case HedgeStates.Dead:
                StateDeadRemain();
                break;
        }

    }

    void StateIdlingEnter()
    {
        stateCurrent = HedgeStates.Idling;

        if (attackCooldown > 0)
        {
            attackCooldown--;
        }
        
        base.agent.destination = transform.position;
    }

    void StateIdlingRemain()
    {
        if (attackCooldown > 0)
        {
            attackCooldown--;
        }
    }

    void StateIdlingExit()
    {

    }

    void StateWhirlingEnter()
    {
        stateCurrent = HedgeStates.Whirling;
        base.animator.SetBool("isWhirling", true);
        base.agent.destination = targetWaypoint.position;

        StartCoroutine(WhirlAttack());
    }

    void StateWhirlingRemain()
    {
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= bufferDistance)
        {
            waypointItterator++;
            whirlCount++;
            if (whirlCount != 3)
            {
                targetWaypoint = waypoints[waypointItterator % waypoints.Length];
                base.agent.destination = targetWaypoint.position;
            }
        }
    }

    void StateWhirlingExit()
    {
        base.animator.SetBool("isWhirling", false);
        attackCooldown = 11;
    }

    void StateHoopingEnter()
    {
        stateCurrent = HedgeStates.Hooping;
        base.animator.SetBool("isHooping", true);
        
        StartCoroutine(HoopAttack());
    }

    void StateHoopingRemain()
    {
       
    }

    void StateHoopingExit()
    {
        base.animator.SetBool("isWhirling", false);
    }

    void StateDeadEnter()
    {
        stateCurrent = HedgeStates.Dead;
        base.agent.destination = transform.position;
        Destroy(gameObject);
    }

    void StateDeadRemain()
    {

    }

    IEnumerator HoopAttack()
    {
        Debug.Log("Hoop Attack");

        yield return new WaitForSeconds(.6f);
        
        foreach (Transform tr in ballSpawns)
        {
            Instantiate(basketballObj, tr.position, new Quaternion(0, 0, 0, 0));
            yield return new WaitForSeconds(.1f);
        }

        yield return new WaitForSeconds(.3f);

        attackCooldown = 11;
        whirlCount = 0;

        yield return null;
    }

    IEnumerator WhirlAttack()
    {
        Debug.Log("Whirl Attack");

        while (stateCurrent == HedgeStates.Whirling)
        {
            Instantiate(whirlAttack, transform.position, new Quaternion(0, 0, 0, 0));
            yield return new WaitForSeconds(.2f);
        }

        attackCooldown = 11;

        yield return null;
    }


    public override void ChangeHealth(float amount)
    {
        if (stateCurrent != HedgeStates.Whirling)
        {
            base.ChangeHealth(amount);
            if (amount < 0)
            {
                ps.Play();
            }
        }
    }
}
