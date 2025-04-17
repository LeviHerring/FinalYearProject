using UnityEngine;

public class DialogueInteractables : InteractableObject
{
    public TextManager textManager;
    public DialogueEntry dialogueEntry;
    private bool dialogueStarted = false;

    void Update()
    {
        if (!dialogueStarted && textManager != null && MouseController.Instance.dialogueInteractable == this)
        {
            dialogueStarted = true;
            textManager.StartDialogue(dialogueEntry);
        }

        if (dialogueStarted && !textManager.dialogueInProgress)
        {
            dialogueStarted = false;
        }
    }
}
