using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddonsDisplayIngame : MonoBehaviour
{
    [SerializeField] private PlayerEquippedAddons equippedAddons = null;
    [SerializeField] private Image addonQ = null;
    [SerializeField] private Image addonE = null;
    [SerializeField] private TMP_Text energyCostQText = null;
    [SerializeField] private TMP_Text energyCostEText = null;

    private void Start()
    {
        List<ObjAbility> addonsList = equippedAddons.GetEquippedAddons();

        addonQ.sprite = addonsList[0].GetIcon();
        addonE.sprite = addonsList[1].GetIcon();

        energyCostQText.text = addonsList[0].GetEnergyCost().ToString();
        energyCostEText.text = addonsList[1].GetEnergyCost().ToString();
    }
}
