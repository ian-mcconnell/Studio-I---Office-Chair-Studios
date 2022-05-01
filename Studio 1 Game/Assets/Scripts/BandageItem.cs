using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//use inheritance
public class BandageItem : Item
{
    //private int maxHeartNumber = 3;
    //public int startHearts = 3;
    //public float currentHealth;
    //private int maxHealth;
    //private int healthPerHeart = 4;
    HealthSystem hs;
    //public string[] itemNames;
    //public Item Bandage;
    InventorySystem inventory;
    // public int itemSlots;
    //public HealthSystem hs;
    //public Image[] healthImages;
    //public Sprite[] healthSprites;
    // Start is called before the first frame update
    
    void Start()
    {
        hs = FindObjectOfType<HealthSystem>();
        inventory = FindObjectOfType<InventorySystem>();

        //itemSlot = 0;

    }
    private void FixedUpdate()
    {
        //itemSlot = inventory.Bandage.itemSlot;
    }

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
   //     itemSlot = inventory.Bandage.itemSlot;
        hs.AddHealth(2);
       
        inventory.itemNames[itemSlot] = null;
        inventory.isFull[itemSlot] = false;
        Debug.Log(itemSlot);
        //for (int i = 0; i < inventory.slots.Length; i++)
        //{
        //    Debug.Log(itemSlot);
        //    //itemNames[i] = inventory.itemNames[i];
        //    if ((inventory.itemNames[i] == "Bandage" && itemSlot == i) )
        //    {
        //        Debug.Log(inventory.itemNames[i] +" "+itemSlot +" " + i);

        //        inventory.itemNames[i] = null;
        //        //inventory.isFull[i] = false;
        //        //               itemSlot = 0;
        //    }
        //}
        Destroy(itemButton);
//        itemButton.gameObject.SetActive(false);
        //if ((inventory.itemNames[itemSlot] == "Bandage" ))
        //    {
        //        inventory.itemNames[itemSlot] = null;
        //        itemSlot = 0;

        //    }

    }
}