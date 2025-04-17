using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTest : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down detected!");
    }
}