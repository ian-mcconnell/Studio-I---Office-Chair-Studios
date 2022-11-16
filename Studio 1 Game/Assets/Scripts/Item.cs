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
    public bool isActive;
    public string itemName;
    public int itemSlot;
    //   public bool reset;
    public void SaveItem()
    {
        SaveSystem.SaveItem(this);
    }
    public void LoadItem()
    {
        //isActive = true;
        ItemData data = SaveSystem.loadItem();
        isActive = data.isActive;
        //level = data.level;
        //currentHealth = data.health;
        ////      Vector3 position;
        ////    position.x = data.

        //direction.x = data.position[0];
        //direction.y = data.position[1];
        //direction.z = data.position[2];
        //transform.position = direction;
        //controller.center = direction - transform.position;
    }
    public void Awake()
    {
        Player = FindObjectOfType<PlayerController>();
        inSys =  FindObjectOfType<InventorySystem>();
        this.gameObject.SetActive(true);
        isActive = true;

        //       itemSlot = 0;
        SaveSystem.items.Add(this);
        SaveItem();
        Debug.Log("is active: " + isActive);
    }

    public void Update()
    {
        float distanceX = Mathf.Abs(transform.position.x - Player.pickUpPosition.position.x);
        float distanceY = Mathf.Abs(transform.position.y - Player.pickUpPosition.position.y);
        float distanceZ = Mathf.Abs(transform.position.z - Player.pickUpPosition.position.z);
        if (isActiveAndEnabled)
        {
            //Debug.Log("is active and enabled: " + isActiveAndEnabled);
            isActive = true;
        }
        else
        {
           // Debug.Log("is active and enabled: " + isActiveAndEnabled);
            isActive = false;
        }
        if(isActive)
        {
            //Debug.Log("is active: " + isActive);
            this.gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log("is active: " + isActive);
            this.gameObject.SetActive(false);
        }
        if (distanceX <= .5f && distanceY <= .5f && distanceZ <= .5f)
        {           
            inSys.addToInventory(this);
            isActive = false;
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            LoadItem();
            //Debug.Log("this code Ran");
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
