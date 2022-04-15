using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject[] slots;
    public bool[] isFull;
   

    public void Update()
    {

    }

    public void inventoryChoice()
    {

    }

    //public void itemUsage()
    //{
    //    switch (Item)
    //    {
    //        case
    //    }
            
    //}



    //public void SaveInv()
    //{
    //   SaveSystem.SaveInv(item);
    //}
    public void resetInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
           isFull[i] = false;
        }
    }

    public void addToInventory(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false )
            {

                isFull[i] = true;
                Instantiate(item.itemButton, slots[i].transform, false);
                SaveSystem.SaveInv(item);
                Destroy(item.gameObject);
                break;
            }
        }
    }

    public void useInventory(int index)
    {

    }
}
        
    
    
    
