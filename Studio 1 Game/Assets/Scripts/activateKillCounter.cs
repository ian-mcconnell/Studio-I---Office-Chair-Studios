using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateKillCounter : MonoBehaviour
{
    public GameObject killCounterA;
    public GameObject killCounterB;
    public GameObject killCounterC;
    public GameObject killCounterD;

    // Update is called once per frame

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "1-A")
        {
            killCounterA.SetActive(true);
            killCounterB.SetActive(false);
            killCounterC.SetActive(false);
            killCounterD.SetActive(false);
            Debug.Log("activated");
        }

        if (collision.gameObject.name == "1-B")
        {
            killCounterA.SetActive(false);
            killCounterB.SetActive(true);
            killCounterC.SetActive(false);
            killCounterD.SetActive(false);
        }
        if (collision.gameObject.name == "1-C")
        {
            killCounterA.SetActive(false);
            killCounterB.SetActive(false);
            killCounterC.SetActive(true);
            killCounterD.SetActive(false);
        }
        if (collision.gameObject.name == "1-D")
        {
            killCounterA.SetActive(false);
            killCounterB.SetActive(false);
            killCounterC.SetActive(false);
            killCounterD.SetActive(true);
        }
    }
}
