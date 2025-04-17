using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public DialogueEntry dialogueEntry;
    public TextMeshProUGUI speechText;
    public float scrollSpeed = 0.02f;
    public bool isFinished;
    public bool dialogueInProgress;

    public int index;
    private bool typing;
    private bool waitingForAnswer;

    void Start()
    {
        isFinished = false;
        gameObject.SetActive(false);
        dialogueInProgress = false;
    }

    void Update()
    {
        if (!dialogueInProgress) return;

        // Click to skip typing
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
        gameObject.SetActive(true);
        isFinished = false;
        dialogueInProgress = true;
        waitingForAnswer = false;

        StartCoroutine(TypeLines());
    }

    IEnumerator TypeLines()
    {
        typing = true;
        speechText.text = "";

        foreach (char c in dialogueEntry.lines[index])
        {
            speechText.text += c;
            yield return new WaitForSeconds(scrollSpeed);
        }

        typing = false;

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
            StartCoroutine(TypeLines());
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
            Debug.Log("Correct Answer!");
        }
        else
        {
            Debug.Log("Wrong Answer!");
        }

        EnableAnswerButtons(false);
        waitingForAnswer = false;
        NextLine();
    }

    void EnableAnswerButtons(bool state)
    {
        foreach (Button button in dialogueEntry.answers)
        {
            button.gameObject.SetActive(state);
            button.onClick.RemoveAllListeners();
            if (state)
            {
                button.onClick.AddListener(() => AnswerSelected(button));
            }
        }
    }

    void EndDialogue()
    {
        isFinished = true;
        dialogueInProgress = false;
        speechText.text = "";
        gameObject.SetActive(false);
        MouseController.Instance.dialogueInteractable = null; // free up for next NPC
    }
}
