using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class killCounter : MonoBehaviour
{
    public PlayerController player;

    // Update is called once per frame
    void OnDestroy()
    {
        player.killCount++;
    }
}
