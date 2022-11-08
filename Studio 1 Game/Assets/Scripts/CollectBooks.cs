using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBooks : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject firstNPC;
    public GameObject secondNPC;
    private int count;
    private GameObject[] Blocked;

    void Start()
    {
        secondNPC.SetActive(false);
        count = 0;
        Blocked = GameObject.FindGameObjectsWithTag("Blocked");
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 3)
        {
            secondNPC.SetActive(true);
            firstNPC.SetActive(false);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //GameObject blocker = null;
       if(other.gameObject.CompareTag("Book"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            Debug.Log(count);
        }

       if(other.gameObject.name.Contains("alex's library npc's_3"))
        {
            foreach (GameObject blocks in Blocked)
                GameObject.Destroy(blocks);
        }
    }

    
}
