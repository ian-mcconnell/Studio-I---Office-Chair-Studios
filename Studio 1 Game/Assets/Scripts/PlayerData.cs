using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public float health;
    public float[] position;
    public bool hasLoadedBefore;


    public PlayerData (PlayerController player)
    {
        hasLoadedBefore = player.hasLoadedBefore;
        level = player.level;
        health = player.currentHealth;
        //vector3 for postion as floats
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
