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
    }

    void OnTriggerEnter(Collider other)
    {
        
        GameObject go = other.gameObject;

        if (go.tag == "Player")
        {
            go.GetComponent<PlayerController>().ChangeHealth(damage);
            hitSource.Play();
            Debug.Log("Dealt damage to player");
            Destroy(gameObject);
        }
    }
}
