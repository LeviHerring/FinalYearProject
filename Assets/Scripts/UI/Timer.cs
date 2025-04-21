using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 

public class Timer : MonoBehaviour
{
    public int sceneIndex; 
    public string EndingText; 
    TextMeshProUGUI text;
    public float time;
    public float timeLeft = 60.0f; 
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>(); 
        time = timeLeft; 
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        text.text = "Time left: " + time.ToString("F1"); 

        if(time <= -1)
        {
            text.text = EndingText; 
            if(time <= -5)
            {
                SceneManager.LoadScene(sceneIndex); 
            }
        }
    }
}
