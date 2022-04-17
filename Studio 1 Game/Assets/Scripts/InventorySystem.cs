using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject[] slots;
    public bool[] isFull;
    public string[] itemNames;
    public Item medKit;
    public Item Bandaid;
    public Item Bandage;

    public void Update()
    {

    }

    public void inventoryChoice()
    {

    }
    public void SaveInven()
    {
        SaveSystem.SaveInv(this);
    }

    public void loadInv()
    {
        InvData data = SaveSystem.loadInv();
        resetInventory();


        //Debug.Log(" " + itemName);
        //Debug.Log(" " + this.itemName);
        for (int i = 0; i < slots.Length; i++)
        {
            if (data.itemNames[i] == "MedKit")
            {

                this.addToInventory(medKit);
            }
            if (data.itemNames[i] == "Bandage")
            {
                this.addToInventory(Bandage);
            }
            if (data.itemNames[i] == "Bandaid")
            {
                this.addToInventory(Bandaid);
            }
            if (data.itemNames[i] == null)
            {
                this.addToInventory(null);
            }
  //          itemNames[i] = null;
        }


    }


    public void resetInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
           isFull[i] = false;
            //medKit.reset = true;
            //Bandage.reset = true;
            //Bandaid.reset = true;
            itemNames[i] = null;
        }
    }

    public void addToInventory(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false )
            {
                itemNames[i] = item.itemName;
                isFull[i] = true;
                Instantiate(item.itemButton, slots[i].transform, false);
 //               item.reset = false;
                item.gameObject.SetActive(false);
                break;
            }
        }
    }



    public void useInventory(int index)
    {

    }
}
        
    
    
    
