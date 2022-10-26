using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalAttack : MonoBehaviour
{
    private readonly float damageAmount = -10f;
    public AudioSource hitSource;
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SendMessage("ChangeHealth", damageAmount);
            //Debug.Log(" " + other.name + " got hit with " + Mathf.Abs(damageAmount) + " of damage");
            //Debug.Log("it ran  :(" + other.gameObject.name);
            hitSource.Play();
        }
    }
}
