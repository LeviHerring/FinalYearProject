using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueInteractables : InteractableObject
{
    public TextManager textManager;
    public DialogueEntry dialogueEntry;
    private bool dialogueStarted = false;

    private bool answeredCorrectly = false;
    private bool inCooldown = false;
    private float cooldownTimer = 0f;

    public int nextSceneIndex = -1; // set this in the inspector per character if needed

    private Collider2D interactableCollider;

    void Start()
    {
        // Get the collider attached to this object
        interactableCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Cooldown countdown
        if (inCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                inCooldown = false;
                Debug.Log("You can now retry this dialogue.");
            }
        }

        if (!dialogueStarted && !answeredCorrectly && !inCooldown && textManager != null && MouseController.Instance.dialogueInteractable == this)
        {
            dialogueStarted = true;
            textManager.StartDialogue(dialogueEntry);
            if (interactableCollider != null)
            {
                interactableCollider.enabled = false;  // Disable collider during dialogue
            }
        }

        if (dialogueStarted && !textManager.dialogueInProgress)
        {
            dialogueStarted = false;

            if (textManager.lastAnswerCorrect)
            {
                answeredCorrectly = true;

                if (nextSceneIndex >= 0)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
            }
            else if (textManager.lastAnswerWrong)
            {
                inCooldown = true;
                cooldownTimer = 0f;
                Debug.Log("Wrong answer. Try again in 30 seconds.");
            }

            // Re-enable the collider when dialogue is finished
            if (interactableCollider != null)
            {
                interactableCollider.enabled = true;
            }
        }
    }
}
