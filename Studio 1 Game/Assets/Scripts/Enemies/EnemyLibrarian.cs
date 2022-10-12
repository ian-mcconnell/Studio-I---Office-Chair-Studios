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
    public GameObject chargerEnemy;
    public GameObject wimplingHordeEnemy;
    public Transform[] screamSpawns;
    public Transform[] bookSpawns;
    public Transform[] monsterSpawns;

    private int attackCooldown = 6;
    private float bookProjectileSpeed = -6f;
    private float screamProjectileSpeed = -10f;
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
        maxHealth = 30f;
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
        Debug.Log("Current state: " + stateCurrent + "HP: " + currentHealth.ToString());
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

        //SCREAM ATTACK
        if (attackSelection == 1)
        {
            ScreamSource.Play();
            base.animator.SetBool("isScreaming", true);
            attacking = true;

            yield return new WaitForSeconds(.3f);

            int screamSpawnIndex = Random.Range(0, screamSpawns.Length);
            GameObject screamAttack = Instantiate(screamObj, screamSpawns[screamSpawnIndex].position, new Quaternion(0f, 90f, 90f, 0));
            screamAttack.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, screamProjectileSpeed);
        }
        //BOOK ATTACK
        else
        {
            base.animator.SetBool("isThrowing", true);

            yield return new WaitForSeconds(.3f);

            int bookSpawnIndex = Random.Range(0, bookSpawns.Length);
            GameObject bookAttack = Instantiate(bookObj, bookSpawns[bookSpawnIndex].position, new Quaternion(0, 0, 0, 0));
            bookAttack.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, bookProjectileSpeed);
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

        //Launch player to start
        Instantiate(hitResponse, transform.position, new Quaternion(0, 0, 0, 0));
        //Kill all enemies too
        NukeEnemiesInStage();

        yield return new WaitForSeconds(.5f);

        //Spawn new enemies
        foreach (Transform spawnPoint in monsterSpawns)
        {
            if (currentHealth == 20f)
            {
                //Second wave
                Instantiate(chargerEnemy, spawnPoint.position, new Quaternion(0, 0, 0, 0));
            }
            else if (currentHealth == 10f)
            {
                //Third wave
                Instantiate(wimplingHordeEnemy, spawnPoint.position, new Quaternion(0, 0, 0, 0));
            }
        }

        yield return new WaitForSeconds(.3f);
        isHit = false;
        yield return null;
    }

    private void NukeEnemiesInStage()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in allEnemies)
        {
            Destroy(enemy);
        }
    }

    public override void ChangeHealth(float amount)
    {
        if (stateCurrent != LibrarianStates.Hit && !isHit)
        {
            base.ChangeHealth(amount);
            if (amount < 0)
            {
                ps.Play();
                currentMaxCooldown -= 1;
                isHit = true;
            }
        }
        Debug.Log("LibrarianHealth: " + currentHealth);
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
