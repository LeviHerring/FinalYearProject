using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShootingMouse : MonoBehaviour
{
    public static ShootingMouse Instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] LayerMask layer;
    public bool isHit;
    [SerializeField] float cooldown; 

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
        if (Input.GetMouseButtonDown(0) /*&& !isHit*/) // Prevent hitting again during cooldown
        {
            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, Mathf.Infinity, layer);

            if (hit.collider != null)
            {
                EmuRunning emu = hit.collider.GetComponent<EmuRunning>();
                if (emu != null)
                {
                    isHit = true;
                    emu.emusKilledText.killed++; 
                    Destroy(emu.gameObject);
                    //StartCoroutine(Cooldown());
                }
            }
        }
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isHit = false; 
    }


}
