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

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            startColor = sprite.color;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (activated)
        {
            if (!hasStarted)
            {
                startColor = sprite.color; // Get starting color on first activation
                hasStarted = true;
            }

            Activated();
        }
    }

    void Activated()
    {
        if (timer <= 15f)
        {
            float t = Mathf.InverseLerp(0f, 15f, timer); // 0 to 1 over 15 seconds
            sprite.color = Color.Lerp(startColor, targetGreenColor, t);
        }
    }
}