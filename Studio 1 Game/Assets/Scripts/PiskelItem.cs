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
 //   public Item Bandaid;
    //public HealthSystem hs;
    //public Image[] healthImages;
    //public Sprite[] healthSprites;
    // Start is called before the first frame update
    void Start()
    {
        hs = FindObjectOfType<HealthSystem>();
        inventory = FindObjectOfType<InventorySystem>();
        itemSlot = inventory.Bandaid.itemSlot;
    }
    private void FixedUpdate()
    {
 //       itemSlot = inventory.Bandaid.itemSlot;
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
        for (int i = 0; i < inventory.slots.Length; i++)
        {

            Debug.Log(itemSlot);
            //itemNames[i] = inventory.itemNames[i];
            if ((inventory.itemNames[i] == "Bandaid" && itemSlot == i) )
            {
                Debug.Log(inventory.itemNames[i] + " " + itemSlot + " " + i);
                inventory.itemNames[i] = null;
 //               itemSlot = 0;
            }
        }
        Destroy(itemButton);
        //itemButton.gameObject.SetActive(false);
        //      itemSlot = inventory.Bandaid.itemSlot;
        //if ((inventory.itemNames[itemSlot] == "Bandaid"))
        //    {
        //        inventory.itemNames[itemSlot] = null;
        //        itemSlot = 0;

        //    }

    }
}