using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DialogueEntry : MonoBehaviour
{
    public string[] lines;
    public bool isQuestion;
    public Button[] answers;
    public Button correctAnswer; 
}
