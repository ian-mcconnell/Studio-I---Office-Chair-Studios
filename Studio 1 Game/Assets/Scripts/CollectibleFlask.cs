using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFlask : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Flask worked
            other.gameObject.SendMessage("SetHasChemical");
            Destroy(gameObject);
        }
    }
}
