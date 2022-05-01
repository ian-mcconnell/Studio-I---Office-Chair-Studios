using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHedge : Enemy
{
    public Transform[] waypoints;
    private int waypointItterator = 0;
    Transform targetWaypoint;

    private HedgeStates stateCurrent = HedgeStates.Idling;

    public float attackRange = 2f;
    public float aggroRange = 5f;

    public GameObject whirlAttack;

    private int attackCooldown = 0;
    private bool attackLocked = false;

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
                else if (playerDistance <= attackRange) { StateIdlingExit(); StateHoopingEnter(); }
                else if (playerDistance <= aggroRange) { StateIdlingExit(); StateWhirlingEnter(); }
                else { StateIdlingRemain(); }
                break;

            case HedgeStates.Whirling:
                if (isDead) { StateWhirlingExit(); StateDeadEnter(); }
                else if (playerDistance <= attackRange) { StateWhirlingExit(); StateIdlingEnter(); }
                else { StateWhirlingRemain(); }
                break;

            case HedgeStates.Hooping:
                if (isDead) { StateHoopingExit(); StateDeadEnter(); }
                else if (playerDistance >= attackRange && playerDistance < aggroRange && !attackLocked) { StateHoopingExit(); StateIdlingEnter(); }
                else { StateHoopingExit(); }
                break;

            case HedgeStates.Dead:
                StateDeadRemain();
                break;
        }

    }

    void StateIdlingEnter()
    {
        stateCurrent = HedgeStates.Idling;
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1f)
        {
            waypointItterator += 1;
        }
        targetWaypoint = waypoints[waypointItterator % waypoints.Length];
        base.agent.destination = targetWaypoint.position;
    }

    void StateIdlingRemain()
    {
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1f)
        {
            waypointItterator += 1;
        }
        targetWaypoint = waypoints[waypointItterator % waypoints.Length];
        base.agent.destination = targetWaypoint.position;
    }

    void StateIdlingExit()
    {

    }

    void StateWhirlingEnter()
    {
        stateCurrent = HedgeStates.Whirling;
        base.agent.destination = player.position;
    }

    void StateWhirlingRemain()
    {
        base.agent.destination = player.position;
    }

    void StateWhirlingExit()
    {

    }

    void StateHoopingEnter()
    {
        stateCurrent = HedgeStates.Hooping;
        if (attackCooldown == 0)
        {
            int randInt = Random.Range(1, 3);
            if (randInt == 1)
            {
                StartCoroutine(SmallAttack());
            }
            else
            {
                //StartCoroutine(LargeAttack());
            }
        }
    }

    void StateHoopingRemain()
    {
        if (attackCooldown == 0)
        {
            int randInt = Random.Range(1, 3);
            if (randInt == 1)
            {
                StartCoroutine(SmallAttack());
            }
            else
            {
                //StartCoroutine(LargeAttack());
            }
        }
    }

    void StateHoopingExit()
    {

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

    IEnumerator SmallAttack()
    {
        Debug.Log("Small Attack");
        attackLocked = true;
        attackCooldown = 19;
        base.animator.SetBool("attackingWeak", true);

        yield return new WaitForSeconds(.6f);
        if (sr.flipX == false)
        {
            //Instantiate(smallAttack, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            //Instantiate(smallAttack, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        }

        yield return new WaitForSeconds(.3f);

        attackLocked = false;
        base.animator.SetBool("attackingWeak", false);
        yield return null;
    }

    IEnumerator WhirlAttack()
    {
        Debug.Log("Large Attack");
        attackLocked = true;
        attackCooldown = 35;
        base.animator.SetBool("attackingStrong", true);
        var targetTemp = base.agent.destination;
        base.agent.destination = transform.position;

        yield return new WaitForSeconds(1.2f);
        if (sr.flipX == false)
        {
            //Instantiate(largeAttack, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            //Instantiate(largeAttack, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        }

        yield return new WaitForSeconds(.1f);

        attackLocked = false;
        base.agent.destination = targetTemp;
        base.animator.SetBool("attackingStrong", false);
        yield return null;
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
