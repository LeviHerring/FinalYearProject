using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        }
        else
        {
            Debug.Log("Wrong answer.");
        }

        EnableAnswerButtons(false);
        waitingForAnswer = false;
        NextLine();
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
    }
}
