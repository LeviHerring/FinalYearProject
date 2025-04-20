using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ostrich : MonoBehaviour
{
    public Animator anim; 
    public FarmingOstrichDialogue dialogueScript;


    private void Start()
    {
        anim = GetComponent<Animator>(); 
    }
    void OnMouseDown()
    {
        dialogueScript.OstrichClicked();
    }
}
