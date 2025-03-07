using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public TextManager textManager;
    public string[] text;
    public bool activated; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            textManager.gameObject.SetActive(true);
            textManager.speech = text;

            // Check if the speech has finished
            if (textManager.index >= textManager.speech.Length - 1)
            {
                if (Input.GetMouseButtonDown(0)) // If player clicks after the speech is done, deactivate
                {
                    activated = false;
                    textManager.gameObject.SetActive(false); // Hide the text manager
                }
            }
        }
    }

}
