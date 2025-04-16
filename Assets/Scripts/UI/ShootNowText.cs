using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ShootNowText : MonoBehaviour
{
    public TextMeshProUGUI shootText; 
    // Start is called before the first frame update
    void Start()
    {
        shootText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Emu"))
        {
            shootText.gameObject.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shootText.gameObject.SetActive(false);
    }
}
