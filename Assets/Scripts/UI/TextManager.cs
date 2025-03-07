using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public string[] speech;
    public TextMeshProUGUI speechText;
    public float scrollSpeed;
    public bool isValidSpot; 

    public int index; 
    // Start is called before the first frame update
    void Start()
    {
        speechText.text = string.Empty;
        startDialogue(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && isValidSpot)
        {
            if (speechText.text == speech[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                speechText.text = speech[index];  
            }

        }

        if(MouseController.Instance.interactable == null)
        {
            isValidSpot = true; 
        }
    }

    public void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLines());

    }

    IEnumerator TypeLines()
    {
        foreach (char c in speech[index].ToCharArray())
        {
            speechText.text += c;
            yield return new WaitForSeconds(scrollSpeed); 
        }
    }

    void NextLine()
    {
        if (index < speech.Length - 1)
        {
            index++;
            speechText.text = string.Empty;
            StartCoroutine(TypeLines()); 
        }
        else
        {
            gameObject.SetActive(false); 
        }
    }
}
