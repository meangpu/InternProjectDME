using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedAddon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image image;
    [SerializeField] private PlayerEquippedAddons.AddonSlot slot;

    private PlayerEquippedAddons equippedAddons;
    private EquippedAddonsDisplay equippedAddonsDisplay;
    private AddonsUIManager uiManager;
    private ObjAbility addonObject;

    private void Awake()
    {
        equippedAddonsDisplay = transform.parent.GetComponent<EquippedAddonsDisplay>();
        uiManager = equippedAddonsDisplay.GetUIManager();
        equippedAddons = equippedAddonsDisplay.GetEquippedAddonsObject();
    }

    public void AssignAbilityObject(ObjAbility ability)
    {
        addonObject = ability;
    }

    public Image GetImage() => image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedAddonsDisplay.IsEquippingAddon()) { return; }

        uiManager.UpdateDescription(addonObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (equippedAddonsDisplay.IsEquippingAddon()) { return; }

        uiManager.HideDescription();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (equippedAddonsDisplay.IsEquippingAddon()) { return; }

        equippedAddons.ClearAbility(slot);
    }
}
