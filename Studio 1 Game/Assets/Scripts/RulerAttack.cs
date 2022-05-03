using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerAttack : MonoBehaviour
{
   private readonly float damageAmount = -10f;
    public AudioSource hitSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SendMessage("ChangeHealth", damageAmount);
            //Debug.Log(" " + other.name + " got hit with " + Mathf.Abs(damageAmount) + " of damage");
            //Debug.Log("it ran  :(" + other.gameObject.name);
            hitSource.Play();
        }
        else if(other.name.Contains("Basketball"))
        {
            //Shoot da hoop
            Vector3 altPos = new Vector3(transform.position.x, 0f, transform.position.z);
            Vector3 dir = other.transform.position - altPos;
            dir = -dir.normalized;
            other.gameObject.GetComponent<Rigidbody>().AddForce(-dir * 3000f);
            other.gameObject.GetComponent<Basketball>().activatedDamage = true;
            Destroy(other.gameObject, 3f);
        }
    }
}
