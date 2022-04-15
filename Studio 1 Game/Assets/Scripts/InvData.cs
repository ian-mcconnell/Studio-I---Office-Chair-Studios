using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InvData 
{
    public string itemName;
    public float[] position;

    public InvData(Item item)
    {
        itemName = item.itemName;

        //itemName = new string[3];
        //itemName[1] = item.itemName;
        //itemName[2] = item.itemName;
        //itemName[3] = item.itemName;

        //position = new float[3];

        //position[0] = item.itemButton.transform.position.x;
        //position[1] = item.itemButton.transform.position.y;
        //position[2] = item.itemButton.transform.position.z;

    }

}
