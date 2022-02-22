using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : Enemy
{
    public Transform[] waypoints;
    private int waypointItterator = 0;
    Transform targetWaypoint;

    private PatrollingStates stateCurrent = PatrollingStates.Patrolling;

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
        
    }

    public override void FSMProcess()
    {
        base.playerDistance = Vector3.Distance(transform.position, player.position);

        switch (stateCurrent)
        {
            case PatrollingStates.Patrolling:
                if (isDead) { StatePatrollingExit(); StateDeadEnter(); }
                else if (playerDistance <= 2f) { StatePatrollingExit(); StateAttackingEnter(); }
                else if (playerDistance <= 10f) { StatePatrollingExit(); StateChasingEnter(); }
                else { StatePatrollingRemain(); }
                break;

            case PatrollingStates.Chasing:
                if (isDead) { StateChasingExit(); StateDeadEnter(); }
                else if (playerDistance <= 2f) { StateChasingExit(); StateAttackingEnter(); }
                else if (playerDistance >= 10f) { StateChasingExit(); StatePatrollingEnter(); }
                else { StateChasingRemain(); }
                break;

            case PatrollingStates.Attacking:
                if (isDead) { StateAttackingExit(); StateDeadEnter(); }
                else if (playerDistance >= 2f && playerDistance < 10f) { StateAttackingExit(); StateChasingEnter(); }
                else if (playerDistance >= 10f) { StateAttackingExit(); StatePatrollingEnter(); }
                else { StateAttackingRemain(); }
                break;

            case PatrollingStates.Dead:
                StateDeadRemain();
                break;
        }
    }

    void StatePatrollingEnter()
    {
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
        base.agent.destination = transform.position;
        Destroy(gameObject, 3f);
    }

    void StateDeadRemain()
    {

    }

    void StateDeadExit()
    {

    }
}
