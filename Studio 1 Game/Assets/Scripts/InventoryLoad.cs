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
            
            var delims =  ",".ToCharArray(); // define delimiters
//            var parenthesis = "()".ToCharArray();
            // split the values in an array
            string[] values  = itemButtonName.Split(delims);
//            Debug.Log(values[0]+"/t"+values[1]+"/t"+values[2] + "/t" + values[3] + "/t" + values[4] + "/t" + values[5] + "/t" + values[6] + "/t" + values[7]);
            var pos = new Vector3
            {
                x = float.Parse(values[1] )+ inSys.slots[1].transform.position.x,
                y = float.Parse(values[2] )+ inSys.slots[1].transform.position.y,
                z = float.Parse(values[3] )+ inSys.slots[1].transform.position.z
            };
            var rot = new Quaternion
            {
                x = float.Parse(values[5]),
                y = float.Parse(values[6]),
                z = float.Parse(values[7]),
                w = float.Parse(values[8])
            };

            //var slot = new Transform
            //{

            //};
 //           inSys.addToInventory(item);
            Instantiate(item.itemButton, pos, rot,inSys.slots[1].transform);
//           Debug.Log("location of button: "+ item.)
        }

    }
}
