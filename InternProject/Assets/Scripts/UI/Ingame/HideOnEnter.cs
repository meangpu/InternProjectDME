using UnityEngine;
using UnityEngine.EventSystems;

public class HideOnEnter : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
