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
    public GameObject phaseOne;

    public int phase = 0;

    private int correctEvidenceCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        textManager.dialogueEntry.lines = startingString;
        textManager.StartDialogue(textManager.dialogueEntry);
    }

    void Update()
    {
        phaseOne.SetActive(phase == 1);
    }

    public void EvidenceSuccess(string name)
    {
        Debug.Log("Correct evidence: " + name);
        correctEvidenceCount++;

        if (correctEvidenceCount >= 2) // You can tweak this
        {
            Debug.Log("Enough evidence presented!");
            StartCoroutine(NextPhaseDelay());
        }
    }

    public void EvidenceFail(string name)
    {
        Debug.Log("Courtroom laughs at " + name);
        // Add moo SFX or laugh SFX here
    }

    IEnumerator NextPhaseDelay()
    {
        yield return new WaitForSeconds(1f);
        ChangePhase();
        textManager.dialogueEntry.lines = new string[] {
            "George Pearce: 'Hmm… that’s... very concerning indeed.'",
            "George Pearce: 'But before I send troops… I have conditions.'"
        };
        textManager.StartDialogue(textManager.dialogueEntry);
    }

    public void ChangePhase()
    {
        panels[phase].SetActive(false);
        phase++;
        panels[phase].SetActive(true);
    }

    public void StartObjectionPhase()
    {
        ChangePhase();
        textManager.dialogueEntry.lines = objectionString;
        textManager.StartDialogue(textManager.dialogueEntry);
    }

    public void Truth()
    {
        Debug.Log("Correct Lie Found!");
        textManager.dialogueEntry.lines = new string[] {
            "Farmer: 'That’s a LIE! The government didn’t pay for ammo — WE DID!'",
            "George Pearce: 'Hmph. You’ve been doing your homework, I see…'"
        };
        textManager.StartDialogue(textManager.dialogueEntry);
        Invoke(nameof(ShowVerdict), 3f);
    }

    public void Lie()
    {
        Debug.Log("Wrong answer!");
        textManager.dialogueEntry.lines = new string[] {
            "George Pearce: 'Nonsense. I absolutely said that.'",
            "*The cow moos judgmentally*"
        };
        textManager.StartDialogue(textManager.dialogueEntry);
    }

    void ShowVerdict()
    {
        ChangePhase();
        textManager.dialogueEntry.lines = verdictString;
        textManager.StartDialogue(textManager.dialogueEntry);
    }

    public void Commence()
    {
        SceneManager.LoadScene("EmuBattlefieldScene"); // replace with your scene name
    }
}