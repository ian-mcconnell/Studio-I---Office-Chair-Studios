using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make this class abstract
public abstract class Item : MonoBehaviour
{
    
   public GameObject itemButton;
    PlayerController Player;
    InventorySystem inSys;
    Item item;
    public string itemName;

 //   public Vector3 buttonPos;
//    public Item item;
    //public Item medKit;
    //public Item bandage;
    //public Item bandaid;

    public void Awake()
    {
        Player = FindObjectOfType<PlayerController>();
        inSys =  FindObjectOfType<InventorySystem>();
        this.gameObject.SetActive(true);
 //       item = FindObjectsOfType<Item>();
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

   // public void loadInv()
   // {
   //     InvData data = SaveSystem.loadInv();
   // //    inSys.resetInventory();
   ////    itemName = data.itemName;
        
   //     //Debug.Log(" " + itemName);
   //     //Debug.Log(" " + this.itemName);
   //     for (int i = 0; i < inSys.slots.Length; i++)
   //     {
   //         if (data.itemNames[i] == "MedKit")
   //         {

   //             inSys.addToInventory(this);
   //         }
   //         if (data.itemNames[i] == "Bandage")
   //         {
   //             inSys.addToInventory(this);
   //         }
   //         if  (data.itemNames[i] == "Bandaid")
   //         {
   //             inSys.addToInventory(this);
   //         }
   //         if (data.itemNames[i] == null)
   //         {
   //             Debug.Log("yay");
   //         }
   //     }
   //     //buttonPos.x = data.position[0];
   //     //buttonPos.y = data.position[1];
   //     //buttonPos.z = data.position[2];
   //     //buttonPos = itemButton.transform.position;

   // }
    //public void SaveInven()
    //{
    //    SaveSystem.SaveInv(inSys);
    //}
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
