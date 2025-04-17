using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform following;
    public Vector3 offset; 

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = following.position + offset; 
    }
}
