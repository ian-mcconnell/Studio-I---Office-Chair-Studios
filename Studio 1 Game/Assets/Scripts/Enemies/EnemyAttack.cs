using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = -2f;
    public float duration = 5f;
    public AudioSource hitSource;

    private void Start()
    {
        Destroy(gameObject, duration);
        Debug.Log("I existed!");
    }

    void OnTriggerEnter(Collider other)
    {
        
        GameObject go = other.gameObject;
        hitSource.Play();
        if (go.tag == "Player")
        {
            go.GetComponent<PlayerController>().ChangeHealth(damage);
            
            //Debug.Log("Dealt damage to player");
            Destroy(gameObject);
        }
    }
}
