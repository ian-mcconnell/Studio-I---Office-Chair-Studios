using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeBookcase : MonoBehaviour
{
    public PlayerController team;
    // Start is called before the first frame update
    void Update()
    {
        if (team.GetComponent<PlayerController>().npcSaved == 3)
        {
            Destroy(gameObject);
        }
    }


}
