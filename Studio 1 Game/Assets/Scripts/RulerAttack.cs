using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerAttack : MonoBehaviour
{
   private readonly float damageAmount = -10f;
    public AudioSource hitSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SendMessage("ChangeHealth", damageAmount);
            Debug.Log(" " + other.name + " got hit with " + Mathf.Abs(damageAmount) + " of damage");
            Debug.Log("it ran  :(" + other.gameObject.name);
            hitSource.Play();
            //Knock back the enemy
            //Vector3 dir = other.transform.position - transform.position;
            //dir = -dir.normalized;
            //GetComponent<Rigidbody>().AddForce(dir * 100f);
        }
  //      Debug.Log("it ran :)" + other.gameObject.name);
    }
}
