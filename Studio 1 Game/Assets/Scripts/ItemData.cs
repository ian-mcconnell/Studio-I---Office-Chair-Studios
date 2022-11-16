using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData 
{
    public bool isActive;
    public ItemData(Item item)
    {
        isActive = item.isActive;
    }
}
