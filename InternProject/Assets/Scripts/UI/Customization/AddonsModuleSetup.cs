using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddonsModuleSetup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;

    private ObjAbility addonObject;

    private AddonsUIManager uiManager;

    private void Awake()
    {
        uiManager = transform.parent.GetComponent<AddonsUIManager>();
    }

    public void DisplayData(ObjAbility addon)
    {
        addonObject = addon;
        image.sprite = addonObject.GetIcon();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiManager.UpdateDescription(addonObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiManager.HideDescription();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiManager.SelectAddonToAssign(addonObject);
    }
}
