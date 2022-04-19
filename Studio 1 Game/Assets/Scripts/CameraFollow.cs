using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform player;
    public Vector3 offset;
    public bool followX, followXandY, followXandZ,followAll;

    void Update()
    {
        if(followX){
            transform.position = new Vector3(player.position.x + offset.x, offset.y, offset.z); // Camera follows the player with specified offset position
        }
        if (followXandY)
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        }
        if (followXandZ)
        {
            transform.position = new Vector3(player.position.x + offset.x, offset.y, player.position.z + offset.z); // Camera follows the player with specified offset position
        }
        if (followAll)
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z); // Camera follows the player with specified offset position
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "followX")
        {
            followX = true;
            followXandY = false;
            followXandZ = false;
            followAll = false;
        }
        if (other.tag == "followXandY")
        {
            followX = false;
            followXandY = true;
            followXandZ = false;
            followAll = false;
        }
        if (other.tag == "followXandZ")
        {
            followX = false;
            followXandY = false;
            followXandZ = true;
            followAll = false;
        }
        if (other.tag == "followAll")
        {
            followX = false;
            followXandY = false;
            followXandZ = false;
            followAll = true;
        }
    }
}
