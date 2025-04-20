using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstrichAmbush : MonoBehaviour
{
    Animator anim;
    private bool isPanicking = false;

    public float slowSpeed = 1f;
    public float mediumSpeed = 2.5f;
    public float fastSpeed = 4f;
    public float panicSpeed = 7f;

    public float escapeX = -12f;  // Position too far left

    int randomSneak;
    float currentSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Run");
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

        if (transform.position.x <= escapeX)
        {
            AmbushGun.Instance?.EmuEscaped();  // Tell gun that one escaped
            Destroy(gameObject);               // Remove the emu
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
