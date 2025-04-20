using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public DialogueEntry dialogueEntry;
    public TextMeshProUGUI speechText;
    public float scrollSpeed = 0.03f;
    public bool isFinished { get; private set; }
    public bool dialogueInProgress { get; private set; }

    private int index = 0;
    private bool typing = false;
    private bool waitingForAnswer = false;

    public bool lastAnswerCorrect = false;
    public bool lastAnswerWrong = false;

    public string correctAnswerFeedback;  // Public string for correct answer feedback message
    public int nextSceneIndex = -1; // Scene index to load after correct answer

    void Update()
    {
        if (!dialogueInProgress) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (typing)
            {
                StopAllCoroutines();
                speechText.text = dialogueEntry.lines[index];
                typing = false;
            }
            else if (!waitingForAnswer)
            {
                NextLine();
            }
        }
    }

    public void StartDialogue(DialogueEntry entry)
    {
        dialogueEntry = entry;
        index = 0;
        isFinished = false;
        dialogueInProgress = true;
        waitingForAnswer = false;
        lastAnswerCorrect = false;
        lastAnswerWrong = false;

        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        typing = true;
        speechText.text = "";

        foreach (char c in dialogueEntry.lines[index])
        {
            speechText.text += c;
            yield return new WaitForSeconds(scrollSpeed);
        }

        typing = false;

        // Check for question
        if (dialogueEntry.isQuestion && index == dialogueEntry.lines.Length - 1)
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
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    public void AnswerSelected(Button selectedButton)
    {
        if (selectedButton == dialogueEntry.correctAnswer)
        {
            Debug.Log("Correct answer.");
            lastAnswerCorrect = true;
            StartCoroutine(ShowCorrectAnswerFeedback());
        }
        else
        {
            Debug.Log("Wrong answer.");
            lastAnswerWrong = true;
            StartCoroutine(ShowWrongAnswerFeedback());
        }

        EnableAnswerButtons(false); // Disable the answer buttons after selection
        waitingForAnswer = false;   // Finish waiting for an answer
    }

    private IEnumerator ShowCorrectAnswerFeedback()
    {
        // Show custom correct answer feedback (can be scene specific)
        speechText.text = correctAnswerFeedback;

        // Wait for a few seconds to show the feedback
        yield return new WaitForSeconds(2f);

        // Proceed to the next line/dialogue or scene
        NextLine();
    }

    private IEnumerator ShowWrongAnswerFeedback()
    {
        // Optionally show a wrong feedback message
        speechText.text = "Oops, that's wrong! Try again!";

        // Wait for a few seconds to show the feedback
        yield return new WaitForSeconds(2f);

        // Optionally, reset the dialogue or re-enable buttons for retry
        EnableAnswerButtons(true); // Allow retrying the question or show the correct feedback
    }

    void EnableAnswerButtons(bool active)
    {
        foreach (Button b in dialogueEntry.answers)
        {
            b.gameObject.SetActive(active);
            b.onClick.RemoveAllListeners();

            if (active)
                b.onClick.AddListener(() => AnswerSelected(b));
        }
    }

    void EndDialogue()
    {
        dialogueInProgress = false;
        isFinished = true;
        speechText.text = "";
        gameObject.SetActive(false);

        if (MouseController.Instance)
            MouseController.Instance.dialogueInteractable = null;

        // If the answer was correct, load the next scene
        if (lastAnswerCorrect)
        {
            if (nextSceneIndex >= 0)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
        else
        {
            // Optionally handle transition if the answer was incorrect
            // Or reset for another try
        }
    }
}
