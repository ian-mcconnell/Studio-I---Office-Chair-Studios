using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharging : Enemy
{
    private ChargingStates stateCurrent = ChargingStates.Idling;

    public float aggroRange = 8f;
    public float locationPadding = 1.2f;
    private int recoveryDelay = 9;
    private int timeRecovering = 0;

    public AudioSource hitSource;

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
        switch (stateCurrent)
        {
            case ChargingStates.Idling:
                if (isDead) { StateIdlingExit(); StateDeadEnter(); }
                else if (playerDistance <= aggroRange) { StateIdlingExit(); StateChargingEnter(); }
                else { StateIdlingRemain(); }
                break;

            case ChargingStates.Charging:
                if (isDead) { StateChargingExit(); StateDeadEnter(); }
                else if (Vector3.Distance(transform.position, base.agent.destination) <= locationPadding) { StateChargingExit(); StateRecoveringEnter(); }
                else { StateChargingRemain(); }
                break;

            case ChargingStates.Recovering:
                if (isDead) { StateRecoveringExit(); StateDeadEnter(); }
                else if (timeRecovering >= recoveryDelay) { StateRecoveringExit(); StateChargingEnter(); }
                else { StateRecoveringRemain(); }
                break;

            case ChargingStates.Dead:
                StateDeadRemain();
                break;
        }
        //Debug.Log(stateCurrent);
    }

    void StateIdlingEnter()
    {
        stateCurrent = ChargingStates.Idling;
        base.agent.destination = transform.position;
    }

    void StateIdlingRemain()
    {
        playerDistance = Vector3.Distance(transform.position, player.position);
        if (player.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void StateIdlingExit()
    {

    }

    void StateChargingEnter()
    {
        base.animator.SetBool("playerDetected", true);
        stateCurrent = ChargingStates.Charging;
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
        stateCurrent = ChargingStates.Recovering;
        base.animator.SetBool("recovering", true);
        base.agent.destination = transform.position;
        timeRecovering = 0;
    }

    void StateRecoveringRemain()
    {
        timeRecovering += 1;
    }

    void StateRecoveringExit()
    {
        base.animator.SetBool("recovering", false);
    }

    void StateDeadEnter()
    {
        stateCurrent = ChargingStates.Dead;
        base.agent.destination = transform.position;
        Destroy(gameObject, 2f);
    }

    void StateDeadRemain()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("ChangeHealth", -3f);
            hitSource.Play();
            //Knock back the player
            //Vector3 dir = other.contacts[0].point - transform.position;
           // dir = -dir.normalized;
            //GetComponent<Rigidbody>().AddForce(dir * 3f);
        }
    }
}
