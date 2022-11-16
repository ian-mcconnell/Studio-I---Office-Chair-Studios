using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ChemicalAttack(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
