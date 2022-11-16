using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScientist : Enemy
{
    private ScientistStates stateCurrent = ScientistStates.Chasing;

    public GameObject spitObj;
    public GameObject spitSpawn;
    public AnimationCurve curve;

    private int attackCooldown = 60;
    private float spitProjectileSpeed = 45f;
    private float sprayProjectileSpeed = 20f;
    private int maxCooldown = 6;
    private int regularAttackCounter = 0;
    private int hitCounter = 0;
    private int maxSprayShots = 20;
    private int maxSpitShots = 4;
    private bool isSwooping = false;
    private bool isSpitting = false;
    private bool isSpraying = false;
    private bool isLowered = false;

    private ParticleSystem ps;
    private SpriteRenderer sr;

    public AudioSource spitSource;

    public Image healthBar;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;

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
        maxHealth = 60f;
        currentHealth = maxHealth;

        StateChasingEnter();
    }

    private void Update()
    {
        if(isLowered)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z);
        }
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
            if(regularAttackCounter == 6)
            {
                isSpraying = true;
                regularAttackCounter = 0;
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
        base.agent.destination = transform.position;

        regularAttackCounter++;
        StartCoroutine(SwoopAttack());
    }

    void StateSwoopingRemain()
    {

    }

    void StateSwoopingExit()
    {
        
    }

    void StateSpittingEnter()
    {
        stateCurrent = ScientistStates.Spitting;

        base.agent.destination = transform.position;
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

        base.agent.destination = transform.position;
        StartCoroutine(SprayAttack());
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
        int spitCount = 0;
        while (spitCount <= maxSpitShots)
        {
            spitCount++;
            spitSpawn.transform.LookAt(player.position + player.gameObject.GetComponent<Rigidbody>().velocity);

            //make pukies
            GameObject spitAttack = Instantiate(spitObj, spitSpawn.transform.position, spitSpawn.transform.rotation);
            spitAttack.GetComponent<Rigidbody>().AddForce(spitAttack.transform.forward * spitProjectileSpeed, ForceMode.Impulse);

            yield return new WaitForSeconds(.2f);
        }

        base.animator.SetBool("isSpitting", false);
        yield return new WaitForSeconds(.4f);

        attackCooldown = maxCooldown;
        isSpitting = false;

        yield return null;
    }

    IEnumerator SwoopAttack()
    {
        Debug.Log("Swoop Attack");

        base.agent.destination = transform.position;
        base.animator.SetBool("isSwooping", true);

        //Swoop at the point behind the player
        float time = 0f;
        float duration = 1f;

        Vector3 start = transform.position;
        //Calculate space behind the player
        Vector3 altPlayerPos = new Vector3(player.position.x, gameObject.transform.position.y, player.position.z);
        Vector3 dir = gameObject.transform.position - altPlayerPos;
        Vector3 end = player.position + dir.normalized;

        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, 0f, heightT);

            transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0f, height, 0f);

            yield return null;
        }

        base.animator.SetBool("isSwooping", false);
        yield return new WaitForSeconds(.3f);

        isSwooping = false;

        yield return null;
    }

    IEnumerator SprayAttack()
    {
        Debug.Log("Spray Attack");
        isSpraying = true;

        float time = 0f;
        float duration = 1f;

        //Lower down to ground level
        Vector3 start = transform.position;
        Vector3 end = new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z);

        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;

            transform.position = Vector3.Lerp(start, end, linearT);

            yield return null;
        }
        
        isLowered = true;
        yield return new WaitForSeconds(0.1f);

        //ratattatatat
        base.animator.SetBool("isSpraying", true);
        spitSpawn.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        int shotsFired = 0;
        float yRot = 0f;
        while (shotsFired <= maxSprayShots)
        {
            shotsFired++;
            GameObject spitAttack = Instantiate(spitObj, spitSpawn.transform.position, spitSpawn.transform.rotation);
            spitAttack.GetComponent<Rigidbody>().AddForce(spitAttack.transform.forward * sprayProjectileSpeed, ForceMode.Impulse);

            yRot += 25f;
            spitSpawn.transform.rotation = Quaternion.Euler(0f, yRot, 0f);
            yield return new WaitForSeconds(.1f);
            yield return null;
        }

        base.animator.SetBool("isSpraying", false);

        //Rise back up
        isLowered = false;
        time = 0f;
        end = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;

            transform.position = Vector3.Lerp(start, end, linearT);

            yield return null;
        }

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
                    maxCooldown -= 1;
                    maxSprayShots *= 2;
                    maxSpitShots *= 2;
                    hitCounter = 0;
                }
            }
        }
        Debug.Log("ScientistHealth: " + currentHealth);
        switch (currentHealth)
        {
            case 50f:
                healthBar.sprite = sprite1;
                break;

            case 40f:
                healthBar.sprite = sprite2;
                break;

            case 30f:
                healthBar.sprite = sprite3;
                break;

            case 20f:
                healthBar.sprite = sprite4;
                break;

            case 10f:
                healthBar.sprite = sprite5;
                break;

            case 0f:
                healthBar.sprite = sprite6;
                endProp.SetActive(true);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().ChangeHealth(-4f);
        }
    }
}