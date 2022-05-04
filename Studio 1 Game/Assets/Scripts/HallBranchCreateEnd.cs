using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallBranchCreateEnd : MonoBehaviour
{
    public PlayerController counter;

    // Update is called once per frame
    void Update()
    {
        if (counter.npcSaved >= 4)
        {
            gameObject.tag = "End";
        }
    }
}
