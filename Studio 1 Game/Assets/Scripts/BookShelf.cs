using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelf : MonoBehaviour
{
    public bool move;
    public float delay = 0;
    public bool rotate;
    public bool disappear;

    public Transform pointA;
    public Transform pointB;
    // Start is called before the first frame update
    void Start()
    {
        if(move == true)
        {
            StartCoroutine(Moving());
        }
        if(rotate == true)
        {
            pointA = gameObject.transform;
            pointB = gameObject.transform;
            StartCoroutine(Rotating());
        }
        if(disappear == true)
        {
            pointA = gameObject.transform;
            pointB = gameObject.transform;
            StartCoroutine(Disappearing());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Moving()
    {
        while (true)
        {
            transform.position = pointA.position;
            yield return new WaitForSeconds(delay + 3);
            transform.position = pointB.position;
            yield return new WaitForSeconds(delay + 3);
            yield return null;
        }
    }

    IEnumerator Rotating()
    {
        while (true)
        {
            transform.Rotate(0, 90, 0);
            yield return new WaitForSeconds(Random.Range(1, 4));
            transform.Rotate(0, -90, 0);
            yield return new WaitForSeconds(Random.Range(1, 4));
            yield return null;
        }
    }

    IEnumerator Disappearing()
    {
        while (true)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
            yield return new WaitForSeconds(delay + 2);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(2);
            yield return null;
        }
    }
}
