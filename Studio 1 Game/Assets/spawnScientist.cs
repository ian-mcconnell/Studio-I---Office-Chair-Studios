using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScientist : MonoBehaviour
{
    public GameObject anim;
    public PlayerController player;
    public GameObject nextTrigger;
    public void playAnimation()
    {
        anim.SetActive(true);
        player.GetComponent<PlayerController>().speed = 0;
    }

    public void spawnNextAnim()
    {
        anim.SetActive(false);
        nextTrigger.SetActive(true);
    }
}
