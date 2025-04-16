using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Timer : MonoBehaviour
{
    TextMeshProUGUI text;
    float time;
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
    }
}
