using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTalking : MonoBehaviour
{
    public dialogueManagement talking;

    IEnumerator destroyObject()
    {
        yield return new WaitWhile(() => talking.isTalking == true);
        Debug.Log("doneTalking");
        Destroy(gameObject);
    }

}
