using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InvData 
{
    public string[] itemNames;

    public InvData( InventorySystem inventory)
    {
        //     if (itemNames.Length >= )
        itemNames = new string[inventory.slots.Length];
        itemNames = inventory.itemNames;
    }

}
