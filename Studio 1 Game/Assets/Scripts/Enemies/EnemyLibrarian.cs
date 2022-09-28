using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLibrarian : Enemy
{
    private LibrarianStates stateCurrent = LibrarianStates.Idling;

    public GameObject hitResponse;
    public GameObject screamObj;
    public GameObject bookObj;
    public Transform[] screamSpawns;
    public Transform[] bookSpawns;

    private int attackCooldown = 6;
    private int currentMaxCooldown = 6;
    private bool isHit = false;
    private bool attacking = false;

    private ParticleSystem ps;
    private SpriteRenderer sr;

    public AudioSource ScreamSource;

    public Image healthBar;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;

    public GameObject endProp;

    public enum LibrarianStates
    {
        Idling = 0,
        Attacking,
        Hit,
        Dead
    }

    protected override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();

        base.Start();
        maxHealth = 300f;
        currentHealth = maxHealth;

        StateIdlingEnter();
    }

    public override void FSMProcess()
    {
        //Lower attack cooldown until it reaches zero
        if (attackCooldown > 0)
        {
            attackCooldown -= 1;
        }

         //Switch structure for controlling state changes 
        switch (stateCurrent)
        {
            case LibrarianStates.Idling:
                if (isDead) { StateIdlingExit(); StateDeadEnter(); }
                else if (isHit) { StateIdlingExit(); StateHitEnter(); }
                else if (attackCooldown == 0) { StateIdlingExit(); StateAttackingEnter(); }
                else { StateIdlingRemain(); }
                break;

            case LibrarianStates.Attacking:
                if (isDead) { StateAttackingExit(); StateDeadEnter(); }
                else if (!attacking) { StateAttackingExit(); StateIdlingEnter(); }
                else { StateAttackingRemain(); }
                break;

            case LibrarianStates.Hit:
                if (isDead) { StateHitExit(); StateDeadEnter(); }
                else if (!isHit) { StateHitExit(); StateIdlingEnter(); }
                else { StateHitRemain(); }
                break;

            case LibrarianStates.Dead:
                StateDeadRemain();
                break;
        }

    }

    void StateIdlingEnter()
    {
        stateCurrent = LibrarianStates.Idling;
    }

    void StateIdlingRemain()
    {
        
    }

    void StateIdlingExit()
    {

    }

    void StateHitEnter()
    {
        stateCurrent = LibrarianStates.Hit;
        base.animator.SetBool("isHit", true);

        StartCoroutine(HitResponse());
    }

    void StateHitRemain()
    {

    }

    void StateHitExit()
    {
        base.animator.SetBool("isHit", false);
    }

    void StateAttackingEnter()
    {
        stateCurrent = LibrarianStates.Attacking;

        StartCoroutine(AttackSequence());
    }

    void StateAttackingRemain()
    {

    }

    void StateAttackingExit()
    {
        
    }

    void StateDeadEnter()
    {
        stateCurrent = LibrarianStates.Dead;
        Destroy(gameObject, .5f);
    }

    void StateDeadRemain()
    {

    }

    IEnumerator AttackSequence()
    {
        int attackSelection = Random.Range(1, 3);

        if (attackSelection == 1)
        {
            Debug.Log("Scream Attack");
            ScreamSource.Play();
            base.animator.SetBool("isScreaming", true);
            attacking = true;

            yield return new WaitForSeconds(.3f);

            int screamSpawnIndex = Random.Range(1, 3);
            Instantiate(screamObj, screamSpawns[screamSpawnIndex].position, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            Debug.Log("Book Attack");
            base.animator.SetBool("isThrowing", true);

            yield return new WaitForSeconds(.3f);

            int bookSpawnIndex = Random.Range(1, 5);
            Instantiate(bookObj, screamSpawns[bookSpawnIndex].position, new Quaternion(0, 0, 0, 0));
        }

        yield return new WaitForSeconds(.1f);
        base.animator.SetBool("isScreaming", false);
        base.animator.SetBool("isThrowing", false);

        attackCooldown = currentMaxCooldown;
        attacking = false;

        yield return null;
    }

    IEnumerator HitResponse()
    {
        Debug.Log("Hit Response");

        while (stateCurrent == LibrarianStates.Hit)
        {
            Instantiate(hitResponse, transform.position, new Quaternion(0, 0, 0, 0));
            yield return new WaitForSeconds(.2f);
        }

        attackCooldown = 11;

        isHit = false;
        yield return null;
    }


    public override void ChangeHealth(float amount)
    {
        if (stateCurrent != LibrarianStates.Hit)
        {
            base.ChangeHealth(amount);
            if (amount < 0)
            {
                ps.Play();
                currentMaxCooldown -= 2;
                isHit = true;
            }
        }
        Debug.Log("LibrarianHealth: " + currentHealth);
        switch (currentHealth)
        {
            case 250f:
                healthBar.sprite = sprite1;
                break;

            case 200f:
                healthBar.sprite = sprite2;
                break;

            case 150f:
                healthBar.sprite = sprite3;
                break;

            case 100f:
                healthBar.sprite = sprite4;
                break;

            case 50f:
                healthBar.sprite = sprite5;
                break;

            case 0f:
                healthBar.sprite = sprite6;
                endProp.SetActive(true);
                break;
        }
    }
}
