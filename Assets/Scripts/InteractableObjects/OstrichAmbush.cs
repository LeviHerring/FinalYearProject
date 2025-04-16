using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstrichAmbush : MonoBehaviour
{
    private bool isPanicking = false;

    public float slowSpeed = 1f;
    public float mediumSpeed = 2.5f;
    public float fastSpeed = 4f;
    public float panicSpeed = 7f;

    int randomSneak;
    float currentSpeed;

    void Start()
    {
        randomSneak = Random.Range(0, 3);

        switch (randomSneak)
        {
            case 0:
                currentSpeed = slowSpeed;
                break;
            case 1:
                currentSpeed = mediumSpeed;
                break;
            case 2:
                currentSpeed = fastSpeed;
                break;
        }
    }

    void Update()
    {
        if (isPanicking)
        {
            MoveLeft(panicSpeed);
        }
        else
        {
            MoveLeft(currentSpeed);
        }
    }

    void MoveLeft(float speed)
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public void Panic()
    {
        isPanicking = true;
    }
}