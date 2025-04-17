using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] LayerMask layer;
    public DialogueInteractables dialogueInteractable;
    public CropScript crop;

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
        if (Input.GetMouseButtonDown(0))
        {
            // If dialogue is happening, don’t allow interaction
            if (MouseController.Instance.dialogueInteractable != null &&
                MouseController.Instance.dialogueInteractable.textManager.dialogueInProgress)
            {
                return;
            }

            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, Mathf.Infinity, layer);

            if (hit.collider != null)
            {
                DialogueInteractables clickedDialogue = hit.collider.GetComponent<DialogueInteractables>();

                if (clickedDialogue != null)
                {
                    dialogueInteractable = clickedDialogue;
                    DialogueClick();
                    return;
                }

                if (hit.collider.GetComponent<CropScript>())
                {
                    crop = hit.collider.GetComponent<CropScript>();
                    CropsActivated();
                }
            }
        }
    }


    void DialogueClick()
    {
        if (dialogueInteractable.activated != true)
        {
            dialogueInteractable.activated = true;
            dialogueInteractable.gameObject.GetComponent<Collider2D>().enabled = false;

        }
        else
        {
            //dialogueInteractable.textManager.isValidSpot = false;
            dialogueInteractable.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    void CropsActivated()
    {
        if (crop.activated != true)
        {
            crop.activated = true;
            crop.timer = 0; 
            crop.gameObject.GetComponent<Collider2D>().enabled = false;

        }
        else
        {
            crop.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
}
