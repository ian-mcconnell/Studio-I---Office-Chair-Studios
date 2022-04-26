using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogueManagement : MonoBehaviour
{
    private Queue<string> sentences;
    public GameObject panel;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueSprite;
    Image icon;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue (Dialogue dialogue, Sprite sprite)
    {
        Time.timeScale = 0.0f;
        sentences.Clear();

        panel.gameObject.SetActive(true);
        icon = dialogueSprite.GetComponent<Image>();
        icon.sprite = sprite;
        

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        panel.gameObject.SetActive(false);
        sentences.Clear();
        Time.timeScale = 1.0f;
    }
}
