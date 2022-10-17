using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    InventorySystem inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(false);
    }
}
