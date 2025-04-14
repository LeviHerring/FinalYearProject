using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingOstrichDialogue : MonoBehaviour
{
    float timer;
    TextManager textManager;
    DialogueEntry dialogue; 
    // Start is called before the first frame update
    void Start()
    {
        textManager = GetComponent<TextManager>();
        dialogue = GetComponent<DialogueEntry>(); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
    }

    void Speaking()
    {
       if(timer >= 0 || timer < 15)
        {

        }
    }
}
