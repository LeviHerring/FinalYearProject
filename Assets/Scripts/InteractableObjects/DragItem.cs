using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        Debug.Log("Schmello!"); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Started dragging: " + gameObject.name);
        originalPosition = transform.position;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Get the object where we dropped the item
        GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;

        if (hitObject != null && hitObject.CompareTag("Evidence"))
        {
            // Check if the object is being dropped in the right zone
            if (gameObject.name.Contains("Emu_Drag") && hitObject.name.Contains("Chalkboard_Left"))
            {
                CourtManager.Instance.EvidenceSuccess(gameObject.name); // Success for Emu in the left drop zone
            }
            else if (gameObject.name.Contains("Crop_Drag") && hitObject.name.Contains("Chalkboard_Middle"))
            {
                CourtManager.Instance.EvidenceSuccess(gameObject.name); // Success for Crops in the middle drop zone
            }
            else if (gameObject.name.Contains("Rabbit_Drag") && hitObject.name.Contains("Chalkboard_Right"))
            {
                CourtManager.Instance.EvidenceSuccess(gameObject.name); // Success for Rabbit in the right drop zone
            }
            else if (gameObject.name.Contains("Hamburger_Drag"))
            {
                // Hamburger shouldn't go anywhere, so we reset its position
                transform.position = originalPosition;
            }
            else
            {
                CourtManager.Instance.EvidenceFail(gameObject.name); // Failure for wrong placement
                transform.position = originalPosition;
            }
        }
        else
        {
            // If we don't hit a valid zone, return the item to its original position
            transform.position = originalPosition;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down detected!");
    }
}
