using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make this class abstract
public abstract class Item : MonoBehaviour
{
    
   public GameObject itemButton;
    PlayerController Player;
    InventorySystem inSys;
    public string itemName;
    public Vector3 buttonPos;
//    public Item item;
    //public Item medKit;
    //public Item bandage;
    //public Item bandaid;

    public void Awake()
    {
        Player = FindObjectOfType<PlayerController>();
        inSys =  FindObjectOfType<InventorySystem>();
    }

    public void Update()
    {
        float distanceX = Mathf.Abs(transform.position.x - Player.pickUpPosition.position.x);
        float distanceY = Mathf.Abs(transform.position.y - Player.pickUpPosition.position.y);
        if (distanceX <= .5f && distanceY <= .5f)
        {
            inSys.addToInventory(this);
        }
    }

    public void loadInv()
    {
        InvData data = SaveSystem.loadInv();
    //    inSys.resetInventory();
       itemName = data.itemName;
        
        Debug.Log(" " + itemName);
        Debug.Log(" " + this.itemName);
        if(itemName == "MedKit"&& this.itemName =="MedKit" )
        {
            
            inSys.addToInventory(this);
        }
        if(itemName == "Bandage" && this.itemName == "Bandage")
        {
            inSys.addToInventory(this);
        }
        if(itemName == "Bandaid" && this.itemName == "Bandaid")
        {
            inSys.addToInventory(this);
        }

        //buttonPos.x = data.position[0];
        //buttonPos.y = data.position[1];
        //buttonPos.z = data.position[2];
        //buttonPos = itemButton.transform.position;

    }
    //public void SaveInv()
    //{
    //    SaveSystem.SaveInv(this);
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        for (int i = 0; i < inventory.slots.Length; i++)
    //        {
    //            if (inventory.isFull[i] == false)
    //            {
    //                inventory.isFull[i] = true;
    //                Instantiate(itemButton, inventory.slots[i].transform, false);
    //                Destroy(gameObject);
    //                break;
    //            }
    //        }
    //    }
    //}
    public abstract void useItem();

}
