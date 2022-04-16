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
        //    inSys.resetInventory();
        //    itemName = data.itemName;

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
                Debug.Log("yay");
            }
        }
        //buttonPos.x = data.position[0];
        //buttonPos.y = data.position[1];
        //buttonPos.z = data.position[2];
        //buttonPos = itemButton.transform.position;

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
                itemNames[i] = item.itemName;
                isFull[i] = true;
                Instantiate(item.itemButton, slots[i].transform, false);

  

 //               var s = item.itemButton.transform.position.ToString() + item.itemButton.transform.rotation.ToString() + slots[i].transform.ToString();
//                var save = s.Replace("(", ",").Replace(")",",");
//                Debug.Log(s);
                //                PlayerPrefs.SetString(item.itemName, save);
                item.gameObject.SetActive(false);
                break;
            }
        }
    }



    public void useInventory(int index)
    {

    }
}
        
    
    
    
