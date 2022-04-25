using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void callDialogue()
    {
        gameObject.GetComponent<dialogueManagement>().StartDialogue(dialogue);
    }
}
