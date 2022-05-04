using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateSaveNPC : MonoBehaviour
{
    public PlayerController player;
    public GameObject npc;
    // Update is called once per frame
    void Update()
    {
        if (player.killCount >= 2)
        {
            npc.SetActive(true);
        }
    }
}
