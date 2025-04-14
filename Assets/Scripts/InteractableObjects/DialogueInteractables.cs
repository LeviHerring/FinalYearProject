using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractables : InteractableObject
{
    public TextManager textManager;
    public DialogueEntry dialogueEntry;
    private bool dialogueStarted = false; //  Prevents re-triggering dialogue every frame
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
