using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedAddonsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerEquippedAddons equippedAddons;
    [SerializeField] private AddonsUIManager uiManager;
    [SerializeField] private Image[] addonIcons;

    private List<ObjAbility> addonsList;

    private void Start()
    {
        equippedAddons.OnUpdateAddon += HandleUpdateAddon;

        addonsList = equippedAddons.GetEquippedAddons();

        addonIcons[0].sprite = addonsList[0].GetIcon();
        addonIcons[1].sprite = addonsList[1].GetIcon();
    }

    private void HandleUpdateAddon(int slotIndex)
    {
        addonIcons[slotIndex].sprite = addonsList[slotIndex].GetIcon();
    }

    private void OnDestroy()
    {
        equippedAddons.OnUpdateAddon -= HandleUpdateAddon;
    }
}
