using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public int Health = 10;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void TakeDamage(int Damage)
    {
        Health -= Damage;
     

        if (Health <= 0)
        {
            Die();
        }
        Debug.Log("health: " + Health);
    }

    private void Die()
    {

        //disable the script and the collider
        GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject, 3);
        this.enabled = false;
    }
}
