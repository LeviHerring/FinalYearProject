using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    public TextMeshProUGUI ammo;
    [SerializeField] int maxAmmo = 12;
    AmbushGun ambushGun; 
    // Start is called before the first frame update
    void Start()
    {
        ambushGun = FindObjectOfType<AmbushGun>();
    }

    // Update is called once per frame
    void Update()
    {
        ammo.text = "Ammo: " + (maxAmmo - ambushGun.kills).ToString() + "/" + maxAmmo.ToString();  

        if(ambushGun.kills == 12)
        {
            ammo.text = "12 killed and now The gun is jammed! You can’t reload... and the emus are going wild!"; 
        }
    }
}
