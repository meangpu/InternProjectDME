using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuAddonDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerEquippedAddons playerEquippedAddons = null;
    [SerializeField] private Image addonPanel = null;
    [SerializeField] private Image imageQ = null;
    [SerializeField] private Image imageE = null;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color highlightedColor;

    private void Start()
    {
        UpdateAddonsDisplay();
    }

    private void OnEnable()
    {
        UpdateAddonsDisplay();
    }

    public void UpdateAddonsDisplay()
    {
        List<ObjAbility> addonsList = playerEquippedAddons.GetEquippedAddons();

        if (addonsList.Count != 2)
        {
            playerEquippedAddons.SetEmpty();
            addonsList = playerEquippedAddons.GetEquippedAddons();
        } 

        imageQ.sprite = addonsList[0].GetIcon();
        imageE.sprite = addonsList[1].GetIcon();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        addonPanel.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        addonPanel.color = defaultColor;
    }
}
