using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharging : Enemy
{
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
                else if (Vector3.Distance(transform.position, base.agent.destination) <= 0.2f) { StateChargingExit(); StateRecoveringEnter(); }
                else { StateChargingRemain(); }
                break;

            case ChargingStates.Recovering:
                if (isDead) { StateRecoveringExit(); StateDeadEnter(); }
                else if (timeRecovering == recoveryDelay) { StateRecoveringExit(); StateChargingEnter(); }
                else { StateRecoveringRemain(); }
                break;

            case ChargingStates.Dead:
                StateDeadRemain();
                break;
        }
    }

    void StateIdlingEnter()
    {
        base.agent.destination = transform.position;
    }

    void StateIdlingRemain()
    {
        
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
        
    }

    void StateChargingExit()
    {
        
    }

    void StateRecoveringEnter()
    {
        base.agent.destination = transform.position;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("ChangeHealth", -1f);
        }
    }
}
