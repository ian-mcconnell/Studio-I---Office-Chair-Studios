using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharging : Enemy
{
    public Transform[] waypoints;
    private int waypointItterator = 0;
    Transform targetWaypoint;

    private ChargingStates stateCurrent = ChargingStates.Idling;

    public float aggroRange = 8f;
    private int recoveryDelay = 3;
    private int timeRecovering = 0;

    public enum ChargingStates
    {
        Idling = 0,
        Charging,
        Recovering,
        Dead
    }

    protected override void Start()
    {
        base.Start();
        StateIdlingEnter();
    }

    public override void FSMProcess()
    {
        base.playerDistance = Vector3.Distance(transform.position, player.position);

        switch (stateCurrent)
        {
            case ChargingStates.Idling:
                if (isDead) { StateIdlingExit(); StateDeadEnter(); }
                else if (playerDistance <= aggroRange) { StateIdlingExit(); StateChargingEnter(); }
                else { StateIdlingRemain(); }
                break;

            case ChargingStates.Charging:
                if (isDead) { StateChargingExit(); StateDeadEnter(); }
                else if (playerDistance <= attackRange) { StateChargingExit(); StateAttackingEnter(); }
                else { StateChargingRemain(); }
                break;

            case ChargingStates.Recovering:
                if (isDead) { StateAttackingExit(); StateDeadEnter(); }
                else if (playerDistance >= attackRange && playerDistance < aggroRange) { StateAttackingExit(); StateChasingEnter(); }
                else { StateAttackingRemain(); }
                break;

            case ChargingStates.Dead:
                StateDeadRemain();
                break;
        }
    }

    void StateIdlingEnter()
    {
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1f)
        {
            waypointItterator += 1;
        }
        targetWaypoint = waypoints[waypointItterator % 5];
        base.agent.destination = targetWaypoint.position;
    }

    void StateIdlingRemain()
    {
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1f)
        {
            waypointItterator += 1;
        }
        targetWaypoint = waypoints[waypointItterator % 5];
        base.agent.destination = targetWaypoint.position;
    }

    void StateIdlingExit()
    {

    }

    void StateChargingEnter()
    {
        base.agent.destination = player.position;
    }

    void StateChargingRemain()
    {
        base.agent.destination = player.position;
    }

    void StateChargingExit()
    {
        
    }

    void StateRecoveringEnter()
    {
        timeRecovering = 0;
    }

    void StateRecoveringRemain()
    {
        timeRecovering += 1;
    }

    void StateRecoveringExit()
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
}
