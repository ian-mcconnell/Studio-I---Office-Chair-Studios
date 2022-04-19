using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
     PlayerController player;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.position = teleportTarget.transform.position;
            player.controller.center = teleportTarget.transform.position - player.transform.position;
            Debug.Log("it teleported");

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
