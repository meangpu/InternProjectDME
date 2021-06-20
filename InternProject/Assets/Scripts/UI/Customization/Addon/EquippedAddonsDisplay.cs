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
    [SerializeField] private GameObject comboDisplay = null;

    private List<ObjAbility> addonsList;

    private void Start()
    {
        equippedAddons.OnUpdateAddon += HandleUpdateAddon;

        addonsList = equippedAddons.GetEquippedAddons();

        selectedAddons[0].GetImage().sprite = addonsList[0].GetIcon();
        selectedAddons[1].GetImage().sprite = addonsList[1].GetIcon();

        selectedAddons[0].AssignAbilityObject(addonsList[0]);
        selectedAddons[1].AssignAbilityObject(addonsList[1]);

        HandleComboDisplay(equippedAddons.CheckForCombo(0, 1));
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

    private void HandleUpdateAddon(int slotIndex, bool isCombo)
    {
        selectedAddons[slotIndex].GetImage().sprite = addonsList[slotIndex].GetIcon();
        selectedAddons[slotIndex].AssignAbilityObject(addonsList[slotIndex]);

        HandleComboDisplay(isCombo);
    }

    private void HandleComboDisplay(bool isCombo)
    {
        switch (isCombo)
        {
            case true:
                comboDisplay.SetActive(true);
                return;
            case false:
                comboDisplay.SetActive(false);
                return;
        }
    }

    private void OnDestroy()
    {
        equippedAddons.OnUpdateAddon -= HandleUpdateAddon;
    }
}
