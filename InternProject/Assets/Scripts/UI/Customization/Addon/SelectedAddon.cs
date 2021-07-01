using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedAddon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image image;
    [SerializeField] private PlayerEquippedAddons.AddonSlot slot;

    [Header("Sounds")]
    [SerializeField] private AssignSound assignSound = null;
    [SerializeField] private ObjSound hoverSound = null;
    [SerializeField] private ObjSound onExitSound = null;
    [SerializeField] private ObjSound removeSound = null;

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

        assignSound.SetAndPlaySound(hoverSound);
        uiManager.UpdateDescription(addonObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (equippedAddonsDisplay.IsEquippingAddon()) { return; }

        assignSound.SetAndPlaySound(onExitSound);
        uiManager.HideDescription();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (equippedAddonsDisplay.IsEquippingAddon()) { return; }

        assignSound.SetAndPlaySound(removeSound);
        equippedAddons.ClearAbility(slot);
    }
}
