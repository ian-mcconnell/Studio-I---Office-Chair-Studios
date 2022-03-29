using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make this class abstract
public class Item : MonoBehaviour
{
    private InventorySystem inventory;
    public GameObject itemButton;
    public Transform Player;
    
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }
    public void Update()
    {
        float distanceX = Mathf.Abs(transform.position.x - Player.position.x);
        float distanceY = Mathf.Abs(transform.position.y - Player.position.y);
        if (distanceX <= .5f && distanceY <= .5f)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

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
    //public abstract void useItem();
    
}
