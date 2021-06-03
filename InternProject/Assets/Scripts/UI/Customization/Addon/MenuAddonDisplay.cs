using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAddonDisplay : MonoBehaviour
{
    [SerializeField] private PlayerEquippedAddons playerEquippedAddons = null;
    [SerializeField] private Image imageQ = null;
    [SerializeField] private Image imageE = null;

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
        imageQ.sprite = addonsList[0].GetIcon();
        imageE.sprite = addonsList[1].GetIcon();
    }
}
