using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLoad: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadInventory(Item item)
    {
        string itemButtonName = PlayerPrefs.GetString(item.itemName, "notfound");
        InventorySystem inSys = FindObjectOfType<InventorySystem>();
        if (itemButtonName != "notfound")
        {

            var delims = ",".ToCharArray();

            string[] values = itemButtonName.Split(delims);

            var pos = new Vector3
            {
                x = float.Parse(values[1]) + inSys.slots[1].transform.position.x,
                y = float.Parse(values[2]) + inSys.slots[1].transform.position.y,
                z = float.Parse(values[3]) + inSys.slots[1].transform.position.z
            };
            var rot = new Quaternion
            {
                x = float.Parse(values[5]),
                y = float.Parse(values[6]),
                z = float.Parse(values[7]),
                w = float.Parse(values[8])
            };

            Instantiate(item.itemButton, pos, rot,inSys.slots[1].transform);

        }

    }
}
