using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFlask : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("SetHasChemical");
        }
    }
}
