using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskSpawner : MonoBehaviour
{
    public float initialDelay = 0f;
    public float repeatDelay = 0f;
    private bool spawning = true;
    public GameObject flaskPrefab;

    void Start()
    {
        Invoke("SpawnFlask", initialDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !spawning)
        {
            spawning = true;
            Invoke("SpawnFlask", repeatDelay);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !spawning)
        {
            spawning = true;
            Invoke("SpawnFlask", repeatDelay);
        }
    }

    private void SpawnFlask()
    {
        spawning = false;
        GameObject newFlask = Instantiate(flaskPrefab, gameObject.transform, true);
        newFlask.transform.position = newFlask.transform.parent.transform.position;
    }
}
