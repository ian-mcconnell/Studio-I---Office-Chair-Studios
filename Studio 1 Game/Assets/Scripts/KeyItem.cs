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
    Wall keyWall;
    public GameObject[] theWall;
    private GameObject player;
    public Text keyInfo;
   
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
        theWall = GameObject.FindGameObjectsWithTag("Wall");
        player = GameObject.FindGameObjectWithTag("Player");
        keyInfo.gameObject.SetActive(false);
        
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
    //public bool CheckforKey()


    public override void useItem()
    {
        //   hs.AddHealth(8);
         
        inventory.itemNames[itemSlot] = null;
        inventory.isFull[itemSlot] = false;
        Debug.Log(itemSlot);
        Debug.Log("Now, unlock a door!");
        //itemNames[i] = inventory.itemNames[i];
        //Debug.Log(inventory.itemNames[i] + " " + itemSlot + " " + i);
        GameObject door = null;
        float closeDistance = float.MaxValue;
        for (int i = 0; i < theWall.Length; i++)
        {
            float distance = Vector3.Distance(player.transform.position, theWall[i].transform.position);
            Debug.Log(distance);
            Debug.Log(player.transform.position);
            Debug.Log(theWall[i].transform.position);
            if (inventory.hasKey == true && distance < 2 && distance < closeDistance)
            {
                door = theWall[i];
                closeDistance = distance;
            }

        }
        if(door != null)
        {
            Debug.Log("You used a key!");
            Destroy(itemButton);
            Debug.Log(door);
            Wall doorwall = door.GetComponent<Wall>();
            Debug.Log(doorwall);
            doorwall.Open();
            Debug.Log("the door is gone");
        }
        //itemButton.gameObject.SetActive(false);
        //       itemSlot = inventory.medKit.itemSlot;
        //if ((inventory.itemNames[itemSlot] == "MedKit"))
        //    {

        //        inventory.itemNames[itemSlot] = null;
        //        itemSlot = 0;

        //    }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Wall keywall = other.GetComponent<Wall>();
    //    if(useItem(item.itemName == "Key"))
    //    {
    //        keywall.Open();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            keyInfo.gameObject.SetActive(true);
            StartCoroutine("WaitForSec");

        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(7);
        Destroy(keyInfo);
    }
}

