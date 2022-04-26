using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject player;
    public Sprite dialogueSprite;


    public void callDialogue()
    {
        player.GetComponent<dialogueManagement>().StartDialogue(dialogue, dialogueSprite);
    }
}
