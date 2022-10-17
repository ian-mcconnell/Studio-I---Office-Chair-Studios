using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basement3Spawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject key;
      public Transform location;
    // Start is called before the first frame update
    void Start()
    {
        
        location = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(key, location);
        
        //key = GameObject.Find("base3key(Clone)");

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    
}
