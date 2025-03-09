using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public TextManager textManager;
    public DialogueEntry dialogueEntry;
    public bool activated;
    private bool dialogueStarted = false; //  Prevents re-triggering dialogue every frame

    void Update()
    {
        if (activated && !dialogueStarted) //  Only start dialogue once per activation
        {
            dialogueStarted = true;
            textManager.StartDialogue(dialogueEntry); //  Pass the DialogueEntry to TextManager
        }

        if (activated && Input.GetMouseButtonDown(0) && !textManager.gameObject.activeSelf) // Close after last line
        {
            activated = false;
            dialogueStarted = false;
        }
    }
}
