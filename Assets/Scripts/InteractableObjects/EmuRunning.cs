using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class EmuRunning : MonoBehaviour
{
    ShootingMouse shootingMouse;
    public float speed = 3f;
    public float randomDirectionChangeInterval = 2f;
    public EmusKilledText emusKilledText; 

    // public Vector2 minBounds = new Vector2(-8.88f, -4.88f);  // Bottom-left of the square
    // public Vector2 maxBounds = new Vector2(8.63f, 4.77f);    // Top-right of the square

    private Vector2 randomDirection;
    private float timer;

    void Start()
    {
        shootingMouse = FindObjectOfType<ShootingMouse>();
        emusKilledText = FindObjectOfType<EmusKilledText>();
        timer = randomDirectionChangeInterval;
        ChooseRandomDirection();
        shootingMouse.isHit = false;
        emusKilledText.totalEmus++; 
    }

    void Update()
    {
        Vector3 movement;

        if (shootingMouse.isHit)
        {
            // Run away from mouse
            Vector3 direction = (transform.position - shootingMouse.transform.position).normalized;
            movement = direction * speed * Time.deltaTime;
        }
        else
        {
            // Random movement
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                ChooseRandomDirection();
                timer = randomDirectionChangeInterval;
            }
            movement = (Vector3)randomDirection * speed * Time.deltaTime;
        }

        // Apply movement
        transform.position += movement;

        // Clamp inside bounds (disabled)
        /*
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            transform.position.z
        );
        */
    }

    void ChooseRandomDirection()
    {
        randomDirection = Random.insideUnitCircle.normalized;
    }
}
