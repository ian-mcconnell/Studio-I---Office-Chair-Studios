using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScientist : Enemy
{
    private ScientistStates stateCurrent = ScientistStates.Chasing;

    public GameObject SwoopResponse;
    public GameObject spitObj;
    public GameObject spitSpawn;
    public AnimationCurve curve;

    private int attackCooldown = 60;
    private float spitProjectileSpeed = -10f;
    private int maxCooldown = 6;
    private int regularAttackCounter = 0;
    private int hitCounter = 0;
    private bool isSwooping = false;
    private bool isSpitting = false;
    private bool isSpraying = false;

    private ParticleSystem ps;
    private SpriteRenderer sr;

    public AudioSource spitSource;

    public Image healthBar;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;

    public GameObject endProp;

    public enum ScientistStates
    {
        Chasing = 0,
        Spitting,
        Swooping,
        Spraying,
        Dead
    }

    protected override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();

        base.Start();
        maxHealth = 30f;
        currentHealth = maxHealth;

        StateChasingEnter();
    }

    public override void FSMProcess()
    {
        //Lower attack cooldown until it reaches zero if not mid attack
        if (attackCooldown > 0 && !isSpitting && !isSwooping)
        {
            attackCooldown -= 1;
        }

        //Look at the player
        if (stateCurrent != ScientistStates.Swooping)
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

        //Switch structure for controlling state changes 
        switch (stateCurrent)
        {
            case ScientistStates.Chasing:
                if (isDead) { StateChasingExit(); StateDeadEnter(); }
                else if (isSwooping) { StateChasingExit(); StateSwoopingEnter(); }
                else if (isSpitting) { StateChasingExit(); StateSpittingEnter(); }
                else if (isSpraying) { StateChasingExit(); StateSprayingEnter(); }
                else { StateChasingRemain(); }
                break;

            case ScientistStates.Spitting:
                if (isDead) { StateSpittingExit(); StateDeadEnter(); }
                else if (!isSpitting) { StateSpittingExit(); StateChasingEnter(); }
                else { StateSpittingRemain(); }
                break;

            case ScientistStates.Swooping:
                if (isDead) { StateSwoopingExit(); StateDeadEnter(); }
                else if (!isSwooping) { StateSwoopingExit(); StateChasingEnter(); }
                else { StateSwoopingRemain(); }
                break;

            case ScientistStates.Spraying:
                if (isDead) { StateSprayingExit(); StateDeadEnter(); }
                else if (!isSpraying) { StateSprayingExit(); StateChasingEnter(); }
                else { StateSprayingRemain(); }
                break;

            case ScientistStates.Dead:
                StateDeadRemain();
                break;
        }
    }

    void StateChasingEnter()
    {
        stateCurrent = ScientistStates.Chasing;
        base.agent.destination = player.position;
    }

    void StateChasingRemain()
    {
        base.agent.destination = player.position;

        if (attackCooldown == 0)
        {
            if(regularAttackCounter == 4)
            {
                isSpraying = true;
            }
            else
            {
                //Coin flip for a regular attack
                int randomizer = Random.Range(0, 2);
                if (randomizer == 0)
                {
                    isSwooping = true;
                }
                else
                {
                    isSpitting = true;
                }
            }
        }
    }

    void StateChasingExit()
    {

    }

    void StateSwoopingEnter()
    {
        stateCurrent = ScientistStates.Swooping;
        base.animator.SetBool("isSwoop", true);
        base.agent.destination = transform.position;

        regularAttackCounter++;
        StartCoroutine(SwoopAttack());
    }

    void StateSwoopingRemain()
    {

    }

    void StateSwoopingExit()
    {
        base.animator.SetBool("isSwoop", false);
    }

    void StateSpittingEnter()
    {
        stateCurrent = ScientistStates.Spitting;

        regularAttackCounter++;
        StartCoroutine(SpitAttack());
    }

    void StateSpittingRemain()
    {

    }

    void StateSpittingExit()
    {

    }

    void StateSprayingEnter()
    {
        stateCurrent = ScientistStates.Spraying;

        regularAttackCounter++;
        StartCoroutine(SpitAttack());
    }

    void StateSprayingRemain()
    {

    }

    void StateSprayingExit()
    {

    }

    void StateDeadEnter()
    {
        stateCurrent = ScientistStates.Dead;
        ps.Play();
        Destroy(gameObject, .5f);
    }

    void StateDeadRemain()
    {
        //Fireworks :)
        ps.Play();
    }

    IEnumerator SpitAttack()
    {
        Debug.Log("Spit Attack");

        spitSource.Play();
        base.animator.SetBool("isSpitting", true);

        yield return new WaitForSeconds(.3f);

        //Fire a spit at where the player will be
        GameObject screamAttack = Instantiate(spitObj, spitSpawn.transform.position, new Quaternion(0f, 90f, 90f, 0));
        screamAttack.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, spitProjectileSpeed);

        yield return new WaitForSeconds(.1f);

        attackCooldown = maxCooldown;
        isSpitting = false;

        yield return null;
    }

    IEnumerator SwoopAttack()
    {
        Debug.Log("Swoop Attack");

        base.agent.destination = player.position;

        //Swoop at the point behind the player
        base.agent.speed += 15f;

        float time = 0f;

        float duration = 1f;
        Vector3 start = transform.position;
        Vector3 end = player.position;

        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, 3.5f, duration); // change 3 to however tall you want the arc to be

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }

        base.agent.speed -= 15f;
        yield return new WaitForSeconds(.3f);

        isSpitting = false;

        yield return null;
    }

    IEnumerator SprayAttack()
    {
        Debug.Log("Spray Attack");

        //Shoot where the player is moving to


        yield return new WaitForSeconds(.3f);

        isSpraying = false;

        yield return null;
    }

    public override void ChangeHealth(float amount)
    {
        if (stateCurrent == ScientistStates.Swooping)
        {
            base.ChangeHealth(amount);
            if (amount < 0)
            {
                ps.Play();
                hitCounter++;
                if (hitCounter == 2)
                {
                    maxCooldown -= 10;
                    hitCounter = 0;
                }
            }
        }
        Debug.Log("ScientistHealth: " + currentHealth);
        switch (currentHealth)
        {
            case 30f:
                healthBar.sprite = sprite1;
                break;

            case 20f:
                healthBar.sprite = sprite2;
                break;

            case 10f:
                healthBar.sprite = sprite3;
                break;

            case 0f:
                healthBar.sprite = sprite4;
                endProp.SetActive(true);
                break;
        }
    }
}