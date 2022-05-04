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
    public int itemSlot;
 //   public bool reset;


    public void Awake()
    {
        Player = FindObjectOfType<PlayerController>();
        inSys =  FindObjectOfType<InventorySystem>();
        this.gameObject.SetActive(true);
 //       itemSlot = 0;
    }

    public void Update()
    {
        float distanceX = Mathf.Abs(transform.position.x - Player.pickUpPosition.position.x);
        float distanceY = Mathf.Abs(transform.position.y - Player.pickUpPosition.position.y);
        float distanceZ = Mathf.Abs(transform.position.z - Player.pickUpPosition.position.z);
        if (distanceX <= .5f && distanceY <= .5f && distanceZ <= .5f)
        {           
            inSys.addToInventory(this);
        }

        //if (reset == true)
        //{
        //    this.gameObject.SetActive(true);
        //}
    }

    public void slotReset( )
    {

    }
    public abstract void useItem();

}
