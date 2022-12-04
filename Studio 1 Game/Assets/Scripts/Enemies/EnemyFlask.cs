using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlask : Enemy
{
    private FlaskStates stateCurrent = FlaskStates.Idling;

    private bool isAttacking = false;
    private bool attackingLeft = true;

    public AudioSource hitSource;
    public GameObject flaskAttack;
    public Transform leftSpawn;
    public Transform rightSpawn;

    private ParticleSystem ps;

    public enum FlaskStates
    {
        Idling = 0,
        Attacking,
        Dead
    }

    protected override void Start()
    {
        base.Start();
        ps = GetComponentInChildren<ParticleSystem>();
        StateIdlingEnter();
    }

    public override void FSMProcess()
    {
        base.playerDistance = Vector3.Distance(transform.position, player.position);
        switch (stateCurrent)
        {
            case FlaskStates.Idling:
                if (isDead) { StateIdlingExit(); StateDeadEnter(); }
                else if (isAttacking) { StateIdlingExit(); StateAttackingEnter(); }
                else { StateIdlingRemain(); }
                break;

            case FlaskStates.Attacking:
                if (isDead) { StateAttackingExit(); StateDeadEnter(); }
                else if (!isAttacking) { StateAttackingExit(); StateIdlingEnter(); }
                else { StateAttackingRemain(); }
                break;

            case FlaskStates.Dead:
                StateDeadRemain();
                break;
        }
    }

    void StateIdlingEnter()
    {
        stateCurrent = FlaskStates.Idling;
    }

    void StateIdlingRemain()
    {

    }

    void StateIdlingExit()
    {
        
    }

    void StateAttackingEnter()
    {
        stateCurrent = FlaskStates.Attacking;
        base.animator.SetBool("isAttacking", true);

        //Face the player
        if (player.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            attackingLeft = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            attackingLeft = false;
        }

        StartCoroutine(FlaskAttack());
    }

    void StateAttackingRemain()
    {
        
    }

    void StateAttackingExit()
    {
        base.animator.SetBool("isAttacking", false);
    }

    void StateDeadEnter()
    {
        stateCurrent = FlaskStates.Dead;
        Destroy(gameObject, 0.1f);
    }

    void StateDeadRemain()
    {

    }

    private IEnumerator FlaskAttack()
    {
        yield return new WaitForSeconds(0.5f);

        if (attackingLeft)
        {
            Instantiate(flaskAttack, leftSpawn);
        }
        else
        {
            Instantiate(flaskAttack, rightSpawn);
        }

        yield return new WaitForSeconds(0.667f);
        isAttacking = true;

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && stateCurrent != FlaskStates.Attacking)
        {
            isAttacking = true;
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
