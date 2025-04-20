using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmingSceneTwo : MonoBehaviour
{
    float timer;
    public TextManager textManager;
    public DialogueEntry dialogue;
    [SerializeField] public string[] speechIntro;

    public GameObject[] ostrich;
    public GameObject[] rabbits;
    public float rabbitSpeed = 2f;

    private bool rabbitsStarted = false;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        Speech();

        if (textManager.isFinished == true)
        {
            SceneManager.LoadScene(6);
        }
    }

    void Speech()
    {
        if (timer >= 5 && !rabbitsStarted)
        {
            textManager.gameObject.SetActive(true);
            dialogue.lines = speechIntro;
            textManager.StartDialogue(dialogue);

            foreach (GameObject rabbit in rabbits)
            {
                StartCoroutine(MoveRabbitToCenter(rabbit));
            }

            rabbitsStarted = true;
        }
    }

    IEnumerator MoveRabbitToCenter(GameObject rabbit)
    {
        Vector3 center = Vector3.zero; // Assuming (0,0,0) is your center
        while (Vector3.Distance(rabbit.transform.position, center) > 0.1f)
        {
            rabbit.transform.position = Vector3.MoveTowards(
                rabbit.transform.position,
                center,
                rabbitSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}