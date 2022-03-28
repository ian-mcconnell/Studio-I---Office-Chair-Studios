using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    public Transform[] waypoints;
    private int waypointItterator = 0;
    Transform targetWaypoint;

    private RangedStates stateCurrent = RangedStates.Patrolling;

    public float attackRange = 10f;
    public float zRange = 0.6f;
    public Transform leftAttackSpawn;
    public Transform rightAttackSpawn;
    public GameObject projectile;
    public float projectileSpeed = 5f;

    public int attackCooldown = 0;
    private bool attackLocked = false;

    public enum RangedStates
    {
        Patrolling = 0,
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
        if (base.player.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        attackCooldown -= 1;
        if (attackCooldown < 0) { attackCooldown = 0; }

        switch (stateCurrent)
        {
            case RangedStates.Patrolling:
                if (isDead) { StatePatrollingExit(); StateDeadEnter(); }
                else if (base.playerDistance <= attackRange && Mathf.Abs(base.player.position.z - transform.position.z) <= zRange) { StatePatrollingExit(); StateAttackingEnter(); }
                else { StatePatrollingRemain(); }
                break;

            case RangedStates.Attacking:
                if (isDead) { StateAttackingExit(); StateDeadEnter(); }
                else if (base.playerDistance >= attackRange || Mathf.Abs(Mathf.Abs(base.player.position.z) - Mathf.Abs(transform.position.z)) >= zRange) { StateAttackingExit(); StatePatrollingEnter(); }
                else { StateAttackingRemain(); }
                break;

            case RangedStates.Dead:
                StateDeadRemain();
                break;
        }
        //Debug.Log(Vector3.Distance(targetWaypoint.position, gameObject.transform.position));
        //Debug.Log(stateCurrent);
        //Debug.Log(Mathf.Abs(base.player.position.z - transform.position.z));
        //Debug.Log(targetWaypoint);
    }

    void StatePatrollingEnter()
    {
        stateCurrent = RangedStates.Patrolling;
        base.animator.SetBool("isAttacking", false);
        base.agent.destination = targetWaypoint.position;
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1.3f)
        {
            waypointItterator += 1;
            targetWaypoint = waypoints[waypointItterator % waypoints.Length];
            //Debug.Log("Reached Waypoint!");
        }
        
    }

    void StatePatrollingRemain()
    {
        if (Vector3.Distance(targetWaypoint.position, transform.position) <= 1.3f)
        {
            waypointItterator += 1;
            targetWaypoint = waypoints[waypointItterator % waypoints.Length];
            base.agent.destination = targetWaypoint.position;
            //Debug.Log("Reached Waypoint!");
        }
    }

    void StatePatrollingExit()
    {

    }

    void StateAttackingEnter()
    {
        stateCurrent = RangedStates.Attacking;
        base.animator.SetBool("isAttacking", true);
        base.agent.destination = transform.position;

        if (attackCooldown == 0 && attackLocked == false)
        {
            if (player.position.x <= transform.position.x)
            {
                //attack left here
                Invoke("ShootProjectileLeft", 1f);
                attackLocked = true;
            }
            else
            {
                //attack right here
                Invoke("ShootProjectileRight", 1f);
                attackLocked = true;
            }
        }
    }

    void StateAttackingRemain()
    {
        if (attackCooldown == 0 && attackLocked == false)
        {
            if (player.position.x <= transform.position.x)
            {
                //attack left here
                Invoke("ShootProjectileLeft", 1f);
                attackLocked = true;
            }
            else
            {
                //attack right here
                Invoke("ShootProjectileRight", 1f);
                attackLocked = true;
            }
        }
    }

    void StateAttackingExit()
    {

    }

    void StateDeadEnter()
    {
        stateCurrent = RangedStates.Dead;
        base.animator.SetBool("isAttacking", false);
        base.agent.destination = transform.position;
        Destroy(gameObject, 3f);
    }

    void StateDeadRemain()
    {

    }

    private void ShootProjectileLeft()
    {
        attackLocked = false;

        GameObject proj = Instantiate(projectile, leftAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        proj.GetComponent<Rigidbody>().velocity = new Vector3(projectileSpeed * -1f, 0f, 0f);
    }

    private void ShootProjectileRight()
    {
        attackLocked = false;

        GameObject proj = Instantiate(projectile, rightAttackSpawn.position, new Quaternion(0, 0, 0, 0));
        proj.GetComponent<Rigidbody>().velocity = new Vector3(projectileSpeed, 0f, 0f);
    }
}
