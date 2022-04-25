using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activateDialogue : MonoBehaviour
{
    public Button button;
    public Canvas dialogue;

    public void activateDialogueCanvas()
    {
        dialogue.gameObject.SetActive(true);
    }
}
