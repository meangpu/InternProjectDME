using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AddonsModuleSetup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;

    [Header("selfValue")]
    [SerializeField] GameObject LockPanel;
    [SerializeField] Button lockButton;
    [SerializeField] GameObject glowCanbuy;
    [SerializeField] TMP_Text unlockPriceText;
    [SerializeField] ParticleSystem unlockPar;

    private AddonsUIManager uiManager;
    private ObjAbility addonObject;

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
