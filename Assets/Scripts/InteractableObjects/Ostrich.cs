using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ostrich : MonoBehaviour
{
    public FarmingOstrichDialogue dialogueScript;

    void OnMouseDown()
    {
        dialogueScript.OstrichClicked();
    }
}
