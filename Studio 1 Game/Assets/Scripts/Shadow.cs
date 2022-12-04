using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    GameObject shadow;
    int layerMask;

    void Start()
    {
        shadow = transform.GetChild(0).gameObject;
        layerMask = LayerMask.GetMask("groundLayer");
    }

    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, layerMask))
        {
            shadow.transform.position = hit.point + new Vector3(0f, 0.01f, 0f);
        }
    }
}