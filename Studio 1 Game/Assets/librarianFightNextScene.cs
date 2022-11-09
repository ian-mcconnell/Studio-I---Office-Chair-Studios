using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class librarianFightNextScene : MonoBehaviour
{

    public GameObject anim;
    public GameObject floor;
    public PlayerController player;
    // Start is called before the first frame update
    public void playAnimation()
    {
        anim.SetActive(true);
        player.GetComponent<PlayerController>().speed = 0;
    }

    public void takeFloorAway()
    {
        floor.SetActive(false);
    }
}
