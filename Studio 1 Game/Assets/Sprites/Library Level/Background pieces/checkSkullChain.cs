using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSkullChain : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().hasSkullChain == true)
        {
            gameObject.tag = "saveNPC";
        }
    }
}
