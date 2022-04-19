using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//use inheritance
public class PiskelItem : Item
{
    //private int maxHeartNumber = 3;
    //public int startHearts = 3;
    //public float currentHealth;
    //private int maxHealth;
    //private int healthPerHeart = 4;
    HealthSystem hs;
    //public string[] itemNames;
    InventorySystem inventory;
    //public HealthSystem hs;
    //public Image[] healthImages;
    //public Sprite[] healthSprites;
    // Start is called before the first frame update
    void Start()
    {
        hs = FindObjectOfType<HealthSystem>();
        inventory = FindObjectOfType<InventorySystem>();

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
        hs.AddHealth(4);
        Destroy(itemButton);
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            //itemNames[i] = inventory.itemNames[i];
            if ((inventory.itemNames[i] == "Bandaid" && itemSlot == i) )
            {
                inventory.itemNames[i] = null;
                itemSlot = 0;
            }
        }
    }
}