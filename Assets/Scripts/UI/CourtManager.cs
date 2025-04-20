using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CourtManager : MonoBehaviour
{
    public static CourtManager Instance;

    public string[] startingString;
    public string[] objectionString;
    public string[] verdictString;

    public TextManager textManager;
    public GameObject[] panels;

    private int phase = 0;
    private int correctEvidenceCount = 0;

    private bool hasTriggeredObjection = false;
    private bool hasShownVerdict = false;

    private void Awake()
    {
        Instance = this;
    }

    IEnumerator Start()
    {
        textManager.gameObject.SetActive(true);
        textManager.dialogueEntry.lines = startingString;
        textManager.StartDialogue(textManager.dialogueEntry);

        // WAIT for dialogue to finish
        yield return new WaitUntil(() => textManager.isFinished);

        //  SMALL DELAY FOR PACE
        yield return new WaitForSeconds(0.5f);

        //  NOW start the first phase (matching)
        panels[phase].SetActive(true);
    }

    public void EvidenceSuccess(string name)
    {
        if (phase != 0) return; // Only count evidence in Phase 0

        correctEvidenceCount++;
        Debug.Log("Correct evidence: " + name);

        if (correctEvidenceCount >= 3)
        {
            StartCoroutine(TransitionToObjection());
        }
    }

    public void EvidenceFail(string name)
    {
        Debug.Log("Courtroom laughs at " + name);
        // Add moo SFX or animation later
    }

    IEnumerator TransitionToObjection()
    {
        panels[phase].SetActive(false);
        phase++;

        yield return new WaitForSeconds(0.5f);

        // Show transition text (e.g. “that’s very concerning…”)
        textManager.dialogueEntry.lines = new string[]
        {
            "George Pearce: 'Hmm… that’s... very concerning indeed.'",
            "George Pearce: 'But before I send troops… I have conditions.'"
        };

        textManager.StartDialogue(textManager.dialogueEntry);

        while (textManager.dialogueInProgress)
            yield return null;

        // Now load objection phase (Phase 2)
        panels[phase].SetActive(true);
        textManager.dialogueEntry.lines = objectionString;
        textManager.StartDialogue(textManager.dialogueEntry);
    }

    public void Truth()
    {
        if (hasTriggeredObjection || hasShownVerdict) return;
        hasTriggeredObjection = true;

        Debug.Log("Correct Lie Found!");

        textManager.dialogueEntry.lines = new string[]
        {
            "Farmer: 'That’s a LIE! The government didn’t pay for ammo — WE DID!'",
            "George Pearce: 'Hmph. You’ve been doing your homework, I see…'"
        };

        textManager.StartDialogue(textManager.dialogueEntry);
        StartCoroutine(WaitForDialogueThenShowVerdict());
    }

    public void Lie()
    {
        if (hasTriggeredObjection || hasShownVerdict) return;
        hasTriggeredObjection = true;

        Debug.Log("Wrong answer!");

        textManager.dialogueEntry.lines = new string[]
        {
            "George Pearce: 'Nonsense. I absolutely said that.'",
            "*The cow moos judgmentally*"
        };

        textManager.StartDialogue(textManager.dialogueEntry);
        StartCoroutine(WaitForDialogueThenShowVerdict());
    }

    IEnumerator WaitForDialogueThenShowVerdict()
    {
        while (textManager.dialogueInProgress)
            yield return null;

        yield return new WaitForSeconds(0.5f);
        ShowVerdict();
    }

    void ShowVerdict()
    {
        if (hasShownVerdict) return;
        hasShownVerdict = true;

        panels[phase].SetActive(false);
        phase++;

        if (phase < panels.Length)
            panels[phase].SetActive(true);

        textManager.dialogueEntry.lines = verdictString;
        textManager.StartDialogue(textManager.dialogueEntry);
    }

    public void Commence()
    {
        SceneManager.LoadScene("9ArmyArrived"); // change to your next scene
    }
}
