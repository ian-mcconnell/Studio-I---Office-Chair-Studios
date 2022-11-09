using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openHazmat : MonoBehaviour
{
    public PlayerController player;
    public GameObject hazmatDoorOpen;
    public GameObject hazmatDoorClosed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().hasSkullChain == true)
        {
            hazmatDoorOpen.SetActive(true);
            hazmatDoorClosed.SetActive(false);
        }
    }
}
