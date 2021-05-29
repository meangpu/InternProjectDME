using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedAddonsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerEquippedAddons equippedAddons;
    [SerializeField] private AddonsUIManager uiManager;
    [SerializeField] private SelectedAddon[] selectedAddons;
    [SerializeField] private GameObject equippingScreen;

    private List<ObjAbility> addonsList;

    private void Start()
    {
        equippedAddons.OnUpdateAddon += HandleUpdateAddon;

        addonsList = equippedAddons.GetEquippedAddons();

        selectedAddons[0].GetImage().sprite = addonsList[0].GetIcon();
        selectedAddons[1].GetImage().sprite = addonsList[1].GetIcon();

        selectedAddons[0].AssignAbilityObject(addonsList[0]);
        selectedAddons[1].AssignAbilityObject(addonsList[1]);
    }

    public AddonsUIManager GetUIManager() => uiManager;

    public PlayerEquippedAddons GetEquippedAddonsObject() => equippedAddons;

    public bool IsEquippingAddon()
    {
        if (equippingScreen.activeInHierarchy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleUpdateAddon(int slotIndex)
    {
        selectedAddons[slotIndex].GetImage().sprite = addonsList[slotIndex].GetIcon();
        selectedAddons[slotIndex].AssignAbilityObject(addonsList[slotIndex]);
    }

    private void OnDestroy()
    {
        equippedAddons.OnUpdateAddon -= HandleUpdateAddon;
    }
}
