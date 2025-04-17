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

        GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;

        if (hitObject != null && hitObject.CompareTag("Evidence"))
        {
            if (gameObject.name.Contains("Crop") || gameObject.name.Contains("Emu"))
            {
                CourtManager.Instance.EvidenceSuccess(gameObject.name);
            }
            else
            {
                CourtManager.Instance.EvidenceFail(gameObject.name);
                transform.position = originalPosition;
            }
        }
        else
        {
            transform.position = originalPosition;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down detected!");
    }
}
