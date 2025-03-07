using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] LayerMask layer;
    public InteractableObject interactable;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances of the manager
        }
    }

    private void Update()
    {
        // Ensure the application is focused
        if (!Application.isFocused)
        {
            return;
        }

        // Convert mouse position to world space
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = -1f; // Ensure it's on the correct plane
        transform.position = mouseWorldPosition;

        // Validate mouse position (prevent clicks outside the screen)
        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 ||
            Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        // Handle mouse input
        MouseInput();
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Get mouse position in world coordinates (2D)
            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Perform a 2D raycast at the mouse position
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, Mathf.Infinity, layer);

            if (hit.collider != null) // Check if something was hit
            {
                Debug.Log("Hit: " + hit.collider.name);

                interactable = hit.collider.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    if(interactable.activated != true)
                    {
                        interactable.activated = true;
                        
                    }
                    else
                    {
                        interactable.textManager.isValidSpot = false;
                    }
                    
                }
            }
            
        }
    }
}
