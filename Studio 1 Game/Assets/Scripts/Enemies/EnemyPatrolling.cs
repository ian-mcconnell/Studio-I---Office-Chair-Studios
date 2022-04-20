using UnityEngine;
using System.Collections;

public class EnemyPatrolling : Enemy
{
    public Transform[] waypoints;
    private int waypointItterator = 0;
    Transform targetWaypoint;

    private PatrollingStates stateCurrent = PatrollingStates.Patrolling;

    public float attackRange = 2f;
    public float aggroRange = 5f;

    private Transform leftAttackSpawn;
    private Transform rightAttackSpawn;

    public GameObject smallAttack;
    public GameObject largeAttack;

    private int attackCooldown = 0;
    private bool attackLocked = false;

    private ParticleSystem ps;
    private SpriteRenderer sr;

    public enum PatrollingStates
    {
        Patrolling = 0,
        Chasing,
        Attacking,
        Dead
    }

    protected override void Start()
    {
        targetWaypoint = waypoints[waypointItterator % waypoints.Length];
        leftAttackSpawn = gameObject.transform.GetChild(1);
        rightAttackSpawn = gameObject.transform.GetChild(2);
        sr = GetComponent<SpriteRenderer>();
        base.Start();
        ps = GetComponentInChildren<ParticleSystem>();
        StatePatrollingEnter();
    }

    public override void FSMProcess()
    {
        base.playerDistance = Vector3.Distance(transform.position, player.position);
        attackCooldown -= 1;
        if (attackCooldown < 0)
        {
            attackCooldown = 0;
        }

        if (stateCurrent != PatrollingStates.Attacking)
        {
            if (base.agent.destination.x < transform.position.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }

        switch (stateCurrent){
            case PatrollingStates.Patrolling:
                if (isDead) { StatePatrollingExit(); StateDeadEnter(); }
                else if (playerDistance <= attackRange) { StatePatrollingExit(); StateAttackingEnter(); }
                else if (playerDistance <= aggroRange) { StatePatrollingExit(); StateChasingEnter(); }
                else { StatePatrollingRemain(); }
                break;

            case PatrollingStates.Chasing:
                if (isDead) { StateChasingExit(); StateDeadEnter(); }
                else if (playerDistance <= attackRange) { StateChasingExit(); StateAttackingEnter(); }
                else if (playerDistance >= aggroRange) { StateChasingExit(); StatePatrollingEnter(); }
                else { StateChasingRemain(); }
                break;

            case PatrollingStates.Attacking:
                if (isDead) { StateAttackingExit(); StateDeadEnter(); }
                else if (playerDistance >= attackRange && playerDistance < aggroRange && !attackLocked) { StateAttackingExit(); StateChasingEnter(); }
                else if (playerDistance >= aggroRange && !attackLocked) { StateAttackingExit(); StatePatrollingEnter(); }
                else { StateAttackingRemain(); }
                break;

            case PatrollingStates.Dead:
                StateDeadRemain();
                break;
        }
        
    }

    void StatePatrollingEnter()
    {
        stateCurrent = PatrollingStates.Patrolling;
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1f)
        {
            waypointItterator += 1;
        }
        targetWaypoint = waypoints[waypointItterator % waypoints.Length];
        base.agent.destination = targetWaypoint.position;
    }

    void StatePatrollingRemain()
    {
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1f)
        {
            waypointItterator += 1;
        }
        targetWaypoint = waypoints[waypointItterator % waypoints.Length];
        base.agent.destination = targetWaypoint.position;
    }

    void StatePatrollingExit()
    {

    }

    void StateChasingEnter()
    {
        stateCurrent = PatrollingStates.Chasing;
        base.agent.destination = player.position;
    }

    void StateChasingRemain()
    {
        base.agent.destination = player.position;
    }

    void StateChasingExit()
    {

    }

    void StateAttackingEnter()
    {
        stateCurrent = PatrollingStates.Attacking;
        if (attackCooldown == 0)
        {
            int randInt = Random.Range(1, 3);
            if (randInt == 1)
            {
                StartCoroutine(SmallAttack());
            }
            else
            {
                StartCoroutine(LargeAttack());
            }
        }
    }

    void StateAttackingRemain()
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
                StartCoroutine(LargeAttack());
            }
        }
    }

    void StateAttackingExit()
    {

    }

    void StateDeadEnter()
    {
        stateCurrent = PatrollingStates.Dead;
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
            Instantiate(smallAttack, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            Instantiate(smallAttack, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        }

        yield return new WaitForSeconds(.3f);

        attackLocked = false;
        base.animator.SetBool("attackingWeak", false);
        yield return null;
    }

    IEnumerator LargeAttack()
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
            Instantiate(largeAttack, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            Instantiate(largeAttack, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
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
