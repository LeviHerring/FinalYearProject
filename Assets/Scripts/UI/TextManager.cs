using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public DialogueEntry dialogueEntry;
    public TextMeshProUGUI speechText;
    public float scrollSpeed;
    public bool isValidSpot;
    public int index;
    private bool waitingForAnswer = false; // Prevents skipping questions

    void Start()
    {
        gameObject.SetActive(false); // Ensures the dialogue UI is hidden until needed
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isValidSpot && !waitingForAnswer) //  Prevent skipping when waiting for an answer
        {
            if (speechText.text == dialogueEntry.lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                speechText.text = dialogueEntry.lines[index];
            }
        }

        if (MouseController.Instance.dialogueInteractable == null)
        {
            isValidSpot = true;
        }
    }

    public void StartDialogue(DialogueEntry entry) // Takes a DialogueEntry as input
    {
        dialogueEntry = entry;
        index = 0;
        waitingForAnswer = false;
        gameObject.SetActive(true); // Show the dialogue UI
        StartCoroutine(TypeLines());
    }

    IEnumerator TypeLines()
    {
        speechText.text = ""; // Reset text before typing
        foreach (char c in dialogueEntry.lines[index].ToCharArray())
        {
            speechText.text += c;
            yield return new WaitForSeconds(scrollSpeed);
        }

        if (dialogueEntry.isQuestion) // If it's a question, enable buttons and wait
        {
            waitingForAnswer = true;
            EnableAnswerButtons(true);
        }
    }

    void NextLine()
    {
        if (index < dialogueEntry.lines.Length - 1)
        {
            index++;
            speechText.text = string.Empty;
            waitingForAnswer = false;
            StartCoroutine(TypeLines());
        }
        else
        {
            EndDialogue();
        }
    }

    public void AnswerSelected(Button selectedButton) // Called when a player selects an answer
    {
        if (selectedButton == dialogueEntry.correctAnswer) // Check correct answer
        {
            Debug.Log("Correct Answer!");
        }
        else
        {
            Debug.Log("Wrong Answer!");
        }

        EnableAnswerButtons(false); // Disable answer buttons after selecting
        waitingForAnswer = false;
        NextLine(); // Move to the next dialogue after selecting
    }

    void EnableAnswerButtons(bool state) //Show/hide answer buttons
    {
        foreach (Button button in dialogueEntry.answers)
        {
            button.gameObject.SetActive(state);
            button.onClick.RemoveAllListeners(); // Remove old listeners
            if (state)
            {
                button.onClick.AddListener(() => AnswerSelected(button)); // Add listener to check answer
            }
        }
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);
    }
}
