using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingOstrichDialogue : MonoBehaviour
{
    float timer;
    public TextManager textManager;
    public DialogueEntry dialogue;
    [SerializeField] public string[] speechIntro;       // Intro lines (0–15s)
    [SerializeField] public string[] speechOstrich;     // Ostrich appears (15–30s)
    [SerializeField] public string[] speechNotLeaving;  // Ostrich won’t leave
    public GameObject ostrich;

    private bool ostrichClicked = false;
    private bool ostrichAppeared = false;
    private bool introStarted = false;
    private bool ostrichDialogueStarted = false;
    private bool notLeavingDialogueStarted = false;

    void Start()
    {
        if (ostrich != null)
            ostrich.SetActive(false); // hide ostrich initially
    }

    void Update()
    {
        timer += Time.deltaTime;
        Speaking();
    }

    void Speaking()
    {
        // INTRO: 0–15 seconds
        if (timer >= 0 && timer < 15 && !introStarted)
        {
            textManager.gameObject.SetActive(true); 
            dialogue.lines = speechIntro;
            textManager.StartDialogue(dialogue);
            introStarted = true;
        }

        // OSTRICH APPEARS: 15–30 seconds
        if (timer >= 15 && timer < 30 && !ostrichDialogueStarted)
        {
            if (ostrich != null)
            {
                ostrich.SetActive(true);
                ostrich.transform.position = new Vector3(6f, 0f, -5f);
            }

            textManager.gameObject.SetActive(true);
            dialogue.lines = speechOstrich;
            textManager.StartDialogue(dialogue);
            ostrichDialogueStarted = true;
        }

        // Ostrich "moves" around — just simple left-right
        if (timer >= 15 && timer < 30 && ostrich != null)
        {
            float move = Mathf.Sin(Time.time * 2f) * 0.05f;
            ostrich.transform.position += new Vector3(move, 0, 0);
        }

        // User clicked ostrich but it didn't go away
        if (ostrichClicked && !notLeavingDialogueStarted)
        {
            textManager.gameObject.SetActive(true);
            dialogue.lines = speechNotLeaving;
            textManager.StartDialogue(dialogue);
            notLeavingDialogueStarted = true;
        }
    }

    void OnMouseDown()
    {
        // Called when this object is clicked — BUT we want this to trigger on the ostrich
    }

    public void OstrichClicked()
    {
        ostrichClicked = true;
    }
}
