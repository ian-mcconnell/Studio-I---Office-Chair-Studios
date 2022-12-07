using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemText : MonoBehaviour
{
    public Text keyInfo;
    // Start is called before the first frame update
    void Start()
    {
        keyInfo.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            keyInfo.gameObject.SetActive(true);
            StartCoroutine("WaitForSec");



        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(7);
        keyInfo.gameObject.SetActive(false);
    }
}
