using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class librarianFightNextScene : MonoBehaviour
{
    public GameObject anim;
    public GameObject floor;
    public PlayerController player;
    private Vector3 endPos;

    bool playing = false;
    float timeFrozen = 2.67f;
    float timePassed = 0f;

    private void LateUpdate()
    {
        if(playing)
        {
            player.gameObject.transform.position = endPos;
            timePassed += Time.deltaTime;
            //Stop the lock after timePassed seconds
            if(timePassed >= timeFrozen)
            {
                playing = false;
            }
        }
    }

    public void Start()
    {
        Debug.Log("playAnimation() ran");
        endPos = player.gameObject.transform.position;
        playing = true;
        anim.SetActive(true);
    }

    public void takeFloorAway()
    {
        floor.SetActive(false);
    }
}
