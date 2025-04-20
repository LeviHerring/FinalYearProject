using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool isSceneTransitioning = false;  // Flag to ensure scene transition only happens once

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
                ostrich.GetComponent<Ostrich>().anim.SetTrigger("Run");
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

        // After the 'notLeaving' dialogue finishes, wait for 2 seconds and go to next scene
        if (notLeavingDialogueStarted && textManager.isFinished && !isSceneTransitioning)
        {
            StartCoroutine(WaitAndGoToNextScene(2f));
            isSceneTransitioning = true;  // Make sure this only happens once
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

    IEnumerator WaitAndGoToNextScene(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for 2 seconds
        SceneManager.LoadScene(4);  // Replace with your desired scene name
    }
}
