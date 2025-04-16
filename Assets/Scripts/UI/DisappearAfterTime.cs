using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DisappearAfterTime : MonoBehaviour
{
    float time;
    public float timePassed = 5; 
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 

        if(time >= timePassed)
        {
            gameObject.SetActive(false); 
        }
    }
}
