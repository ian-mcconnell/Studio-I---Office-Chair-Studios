using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : Enemy
{
    public Transform[] waypoints;
    private int waypointItterator = 0;
    Transform targetWaypoint;

    private PatrollingStates stateCurrent = PatrollingStates.Patrolling;

    public float attackRange = 1f;
    public float aggroRange = 5f;

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
        base.Start();
        StatePatrollingEnter();
    }

    public override void FSMProcess()
    {
        base.playerDistance = Vector3.Distance(transform.position, player.position);

        switch (stateCurrent)
        {
            case PatrollingStates.Patrolling:
                if (isDead) { StatePatrollingExit(); StateDeadEnter(); }
                else if (playerDistance <= attackRange) { StatePatrollingExit(); StateAttackingEnter(); }
                else if (playerDistance <= aggroRange) { StatePatrollingExit(); StateChasingEnter(); }
                else { StatePatrollingRemain(); }
                break;

            case PatrollingStates.Chasing:
                if (isDead) { StateChasingExit(); StateDeadEnter(); }
                else if (playerDistance <= attackRange) { StateChasingExit(); StateAttackingEnter(); }
                else { StateChasingRemain(); }
                break;

            case PatrollingStates.Attacking:
                if (isDead) { StateAttackingExit(); StateDeadEnter(); }
                else if (playerDistance >= attackRange && playerDistance < aggroRange) { StateAttackingExit(); StateChasingEnter(); }
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
        targetWaypoint = waypoints[waypointItterator % 5];
        base.agent.destination = targetWaypoint.position;
    }

    void StatePatrollingRemain()
    {
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1f)
        {
            waypointItterator += 1;
        }
        targetWaypoint = waypoints[waypointItterator % 5];
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
        player.gameObject.SendMessage("ChangeHealth", -10f);
    }

    void StateAttackingRemain()
    {
        player.gameObject.SendMessage("ChangeHealth", -10f);
    }

    void StateAttackingExit()
    {

    }

    void StateDeadEnter()
    {
        stateCurrent = PatrollingStates.Dead;
        base.agent.destination = transform.position;
        Destroy(gameObject, 3f);
    }

    void StateDeadRemain()
    {

    }
}
