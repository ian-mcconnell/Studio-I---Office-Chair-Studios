using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    public Transform[] waypoints;
    private int waypointItterator = 0;
    Transform targetWaypoint;

    private RangedStates stateCurrent = RangedStates.Patrolling;

    public float attackRange = 100f;
    public float zRange = 1f;

    public int attackCooldown = 0;

    public enum RangedStates
    {
        Patrolling = 0,
        Attacking,
        Dead
    }

    protected override void Start()
    {
        targetWaypoint = waypoints[waypointItterator % waypoints.Length];
        base.Start();
        StatePatrollingEnter();
    }

    public override void FSMProcess()
    {
        base.playerDistance = Vector3.Distance(transform.position, player.position);

        switch (stateCurrent)
        {
            case RangedStates.Patrolling:
                if (isDead) { StatePatrollingExit(); StateDeadEnter(); }
                else if (playerDistance <= attackRange && Mathf.Abs(base.player.position.z - transform.position.z) <= zRange) { StatePatrollingExit(); StateAttackingEnter(); }
                else { StatePatrollingRemain(); }
                break;

            case RangedStates.Attacking:
                if (isDead) { StateAttackingExit(); StateDeadEnter(); }
                else if (playerDistance >= attackRange || Mathf.Abs(base.player.position.z - transform.position.z) >= zRange) { StateAttackingExit(); StatePatrollingEnter(); }
                else { StateAttackingRemain(); }
                break;

            case RangedStates.Dead:
                StateDeadRemain();
                break;
        }
        //Debug.Log(Vector3.Distance(targetWaypoint.position, gameObject.transform.position));
        Debug.Log(stateCurrent);
        //Debug.Log(targetWaypoint);
    }

    void StatePatrollingEnter()
    {
        stateCurrent = RangedStates.Patrolling;
        base.agent.destination = targetWaypoint.position;
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1.3f)
        {
            waypointItterator += 1;
            targetWaypoint = waypoints[waypointItterator % waypoints.Length];
            //Debug.Log("Reached Waypoint!");
        }
        
    }

    void StatePatrollingRemain()
    {
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1.3f)
        {
            waypointItterator += 1;
            targetWaypoint = waypoints[waypointItterator % waypoints.Length];
            base.agent.destination = targetWaypoint.position;
            //Debug.Log("Reached Waypoint!");
        }
    }

    void StatePatrollingExit()
    {

    }

    void StateAttackingEnter()
    {
        stateCurrent = RangedStates.Attacking;
        base.agent.destination = transform.position;
        if (attackCooldown == 0)
        {
            if (player.position.x <= transform.position.x)
            {
                //attack left here
            }
        }
    }

    void StateAttackingRemain()
    {
        if (attackCooldown == 0)
        {
            if (player.position.x <= transform.position.x)
            {
                //attack right here
            }
        }
    }

    void StateAttackingExit()
    {

    }

    void StateDeadEnter()
    {
        stateCurrent = RangedStates.Dead;
        base.agent.destination = transform.position;
        Destroy(gameObject, 3f);
    }

    void StateDeadRemain()
    {

    }
}
