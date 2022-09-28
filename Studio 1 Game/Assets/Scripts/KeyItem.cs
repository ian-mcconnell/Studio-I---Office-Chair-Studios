using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//use inheritance
public class KeyItem : Item
{
    //private int maxHeartNumber = 3;
    //public int startHearts = 3;
    //public float currentHealth;
    //private int maxHealth;
    //private int healthPerHeart = 4;
    //HealthSystem hs;
    //public string[] itemNames;
    InventorySystem inventory;
    //    public Item MedKit;
    //public HealthSystem hs;
    //public Image[] healthImages;
    //public Sprite[] healthSprites;
    // Start is called before the first frame update
    PlayerController Pc;
    void Start()
    {
        //hs = FindObjectOfType<HealthSystem>();
        inventory = FindObjectOfType<InventorySystem>();
        //itemSlot = 0;
    }
    private void FixedUpdate()
    {
        //itemSlot = inventory.medKit.itemSlot;
    }
    //public void loadInv()
    //{
    //    InvData data = SaveSystem.loadInv();

    //    itemName = data.itemName;


    //    buttonPos.x = data.position[0];
    //    buttonPos.y = data.position[1];
    //    buttonPos.z = data.position[2];
    //    buttonPos = itemButton.transform.position;
    //    Debug.Log("INVENTORY BUTTON POS: " + buttonPos);

    //}
    //public void SaveInv()
    //{
    //    SaveSystem.SaveInv(this);
    //}

    //void checkHealth()
    //{
    //    for(int i = 0; i < maxHeartNumber; i++)
    //    {
    //        if(s)
    //    }
    //}
    // Update is called once per frame



    public override void useItem()
    {
     //   hs.AddHealth(8);
        inventory.itemNames[itemSlot] = null;
        inventory.isFull[itemSlot] = false;
        Debug.Log(itemSlot);
        //itemNames[i] = inventory.itemNames[i];
        //Debug.Log(inventory.itemNames[i] + " " + itemSlot + " " + i);
        Destroy(itemButton);
        //itemButton.gameObject.SetActive(false);
        //       itemSlot = inventory.medKit.itemSlot;
        //if ((inventory.itemNames[itemSlot] == "MedKit"))
        //    {

        //        inventory.itemNames[itemSlot] = null;
        //        itemSlot = 0;

        //    }

    }
}

