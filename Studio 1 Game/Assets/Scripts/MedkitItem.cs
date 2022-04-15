using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//use inheritance
public class MedkitItem : Item
{
    //private int maxHeartNumber = 3;
    //public int startHearts = 3;
    //public float currentHealth;
    //private int maxHealth;
    //private int healthPerHeart = 4;
    HealthSystem hs;
    //public HealthSystem hs;
    //public Image[] healthImages;
    //public Sprite[] healthSprites;
    // Start is called before the first frame update
    void Start()
    {
        hs = FindObjectOfType<HealthSystem>();
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
    public void loadButton()
    {
        loadInv();
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
        hs.AddHealth(8);
        Destroy(itemButton);
    }
}
