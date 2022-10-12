using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarianHitResponse : MonoBehaviour
{
    public float duration = .2f;
    public AudioSource hitSource;
    public float velZ = -50f;
    public float velY = 5f;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        hitSource.Play();
        if (go.tag == "Player")
        {
            hitSource.Play();

            //Launch the player
            //go.GetComponent<Rigidbody>().velocity = new Vector3(0f, velY, velZ);
            //Debug.Log("HIT RESPONSE CONTACT: " + go.GetComponent<Rigidbody>().velocity.ToString());

            //Temporary Solution
            go.transform.position = GameObject.Find("ResetPoint").transform.position;

            Destroy(gameObject);
        }
    }
}