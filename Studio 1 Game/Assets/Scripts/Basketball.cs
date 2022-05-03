using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    public bool activatedDamage = false;
    private bool dangerToPlayer = true;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (dangerToPlayer)
        {
            if (go.name.Contains("Floor") || go.name.Contains("Attack"))
            {
                dangerToPlayer = false;
            }
            else if (go.tag == "Player")
            {
                go.GetComponent<PlayerController>().ChangeHealth(-3f);
            }
        }
        else if ((go.tag == "Enemy" || go.tag == "Boss") && activatedDamage)
        {
            go.GetComponentInParent(typeof(Enemy)).SendMessage("ChangeHealth", -50f);
            Debug.Log("basketball hit boss");
            Destroy(gameObject);
        }
    }
}
