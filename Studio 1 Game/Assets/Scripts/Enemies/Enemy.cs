using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    NavMeshAgent agent;

    private float maxHealth = 100f;
    private float currentHealth = 100f;
    private bool isDead = false;
    private float playerDistance = Mathf.Infinity;
    public Transform player;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("FSMProcess", 0f, 0.3f);
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
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
