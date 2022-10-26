using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    public float damage = -1000f;
    public AudioSource hitSource;


    void OnTriggerEnter(Collider other)
    {

        GameObject go = other.gameObject;

        if (go.tag == "Player")
        {
            hitSource.Play();
            go.GetComponent<PlayerController>().ChangeHealth(damage);

        }
    }
}
