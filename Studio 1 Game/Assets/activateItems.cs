using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateItems : MonoBehaviour
{
    public GameObject itemA;
    public GameObject itemB;
    public GameObject itemC;
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            itemA.SetActive(true);
            itemB.SetActive(true);
            itemC.SetActive(true);

            Debug.Log("activated");
        }
    }

}
