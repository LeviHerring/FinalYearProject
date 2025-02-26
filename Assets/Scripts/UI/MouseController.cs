using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;

public class MouseController : MonoBehaviour
{

    public static MouseController Instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] LayerMask layer;
    




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

    private void Start()
    {
    }

    private void Update()
    {
        Debug.Log(mainCamera.ScreenToWorldPoint(Input.mousePosition)); 
        // Ensure the application is focused
        if (!Application.isFocused)
        {
            return;
        }
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition; 

        // Validate mouse position
        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 ||
            Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
        {
            return;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layer))
        {
            transform.position = raycastHit.point;
        }


        MouseInput();

    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
          

            // Perform a raycast to determine the world position for spawning
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layer))
            {
              
            }
        }
    }

  

}
