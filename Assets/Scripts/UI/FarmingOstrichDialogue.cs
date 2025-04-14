using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingOstrichDialogue : MonoBehaviour
{
    float timer;
    TextManager textManager;
    DialogueEntry dialogue;
    [SerializeField] public string[] speech;
    public GameObject ostrich; 
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
        Speaking(); 
    }

    void Speaking()
    {
       if(timer >= 0 || timer < 15)
        {

        }
        if (timer >= 16 || timer < 30)
        {
            ostrich.transform.position = new Vector2(6.0f, 0f);  
        }
        if (timer >= 5 || timer < 45)
        {
            if(ostrich.transform.position.x >= 6.0f)
            {
                ostrich.transform.position = Vector2.Lerp(ostrich.transform.position, new Vector2(ostrich.transform.position.x + 10, 0.0f), 3.0f);
            }
          
            if(ostrich.transform.position.x >= -16.0f)
            {
                ostrich.transform.position = Vector2.Lerp(ostrich.transform.position, new Vector2(ostrich.transform.position.x - 10, 0.0f), 3.0f);
            }
        }
    }
}
