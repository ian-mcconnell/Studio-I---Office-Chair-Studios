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

    public void Start()
    {
        //medKit.itemSlot = 0;
        //Bandaid.itemSlot = 0;
        //Bandage.itemSlot = 0;
    }
    public void Update()
    {
        //for (int i = 0; i < slots.Length; i++)
        //{
            //          itemNames[i] = item.itemName;
            //Debug.Log(i);
            //Debug.Log(itemNames[i]);
        //    if (itemNames[i] == null)
        //    {
        //        isFull[i] = false;
        //    }
        //}

    }

    public void inventoryChoice()
    {

    }
    public void SaveInven()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //          itemNames[i] = item.itemName;
            //Debug.Log(i);
            //Debug.Log(itemNames[i]);
        }
            SaveSystem.SaveInv(this);
    }

    public void loadInv()
    {
        InvData data = SaveSystem.loadInv();
        resetInventory();
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    itemNames[i] = data.itemNames[i];
        //    if (itemNames[i] == "MedKit")
        //    {

        //        this.addToInventory(medKit);
        //    }
        //    if (itemNames[i] == "Bandage")
        //    {
        //        this.addToInventory(Bandage);
        //    }
        //    if (itemNames[i] == "Bandaid")
        //    {
        //        this.addToInventory(Bandaid);
        //    }
        //    if (itemNames[i] == null)
        //    {
        //        //               this.addToInventory(null);
        //    }
        //    //          itemNames[i] = null;
        //}

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
                //               this.addToInventory(null);
                itemNames[i] = null;
            }
            else
            {
                itemNames[i] = null;
            }
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
            //medKit.itemButton.gameObject.SetActive(false);
            //Bandaid.itemButton.gameObject.SetActive(false);
            //Bandage.itemButton.gameObject.SetActive(false);
            Transform[] ts = slots[i].GetComponentsInChildren<Transform>();
            foreach (Transform t in ts)
            {
                if (t == true && (t.name == "medkitButton(Clone)" || t.name == "BandageButton(Clone)" || t.name == "PiskelButton(Clone)") )
                {
                    t.gameObject.SetActive(false);
                    Debug.Log(" " + t.name);
                }
            }
 //           itemNames[i] = null;
        }
    }

    public void addToInventory(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //if (itemNames[i] == null)
            //{
            //    isFull[i] = false;
            //}
            if (isFull[i] == false )
            {
                itemNames[i] = item.itemName;
                item.itemSlot = i;

//                item.itemButton.itemSlot = i;
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
        
    
    
    
