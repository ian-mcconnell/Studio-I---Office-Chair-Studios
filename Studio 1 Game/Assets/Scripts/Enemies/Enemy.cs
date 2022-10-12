using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    protected NavMeshAgent agent;

    protected float maxHealth;
    protected float currentHealth;
    protected bool isDead;
    protected float playerDistance;
    protected Transform player;

    protected Animator animator;

    protected virtual void Start()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
        isDead = false;
        playerDistance = Mathf.Infinity;

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        InvokeRepeating("FSMProcess", 0f, 0.1f);
    }

    public virtual void FSMProcess()
    {
        
    }

    public virtual void ChangeHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }

        if (amount < 0)
        {
            SendMessage("Flash");
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
