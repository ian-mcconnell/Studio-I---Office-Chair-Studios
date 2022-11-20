using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBasement : MonoBehaviour
{

    public GameObject endBox;
    public GameObject basementText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Lilith")
        {
            endBox.SetActive(true);
            Debug.Log("activated end");
            basementText.SetActive(false);
        }
    }
}
