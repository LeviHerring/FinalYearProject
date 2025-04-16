using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class EmusKilledText : MonoBehaviour
{
    public TextMeshProUGUI killedText;
    public float killed;
    public float totalEmus; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        killedText.text = "Emus Killed: " + killed.ToString() + "/" + totalEmus.ToString(); 
    }
}
