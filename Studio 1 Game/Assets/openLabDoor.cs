using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openLabDoor : MonoBehaviour
{
    public PlayerController player;
    public GameObject labDoorOpen;
    public GameObject labDoorClosed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().scienceLabopen == true)
        {
            labDoorOpen.SetActive(true);
            labDoorClosed.SetActive(false);
        }
    }
}
