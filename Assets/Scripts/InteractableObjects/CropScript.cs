using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : InteractableObject
{
    private SpriteRenderer sprite;
    public float timer;
    private Color startColor;
    public Color targetGreenColor = Color.green;
    private bool hasStarted = false;
    private bool isAnimating = false;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            startColor = sprite.color;
        }
    }

    void Update()
    {
        if (activated && !hasStarted)
        {
            hasStarted = true;
            StartCoroutine(Activated());
        }

        if (hasStarted)
        {
            timer += Time.deltaTime;
        }
    }

    IEnumerator Activated()
    {
        isAnimating = true;
        anim.SetTrigger("Water");
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("Crop");
        isAnimating = false;
    }
}
