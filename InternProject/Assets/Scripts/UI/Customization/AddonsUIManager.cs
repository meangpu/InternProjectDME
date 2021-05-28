using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddonsUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text addonName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private TMP_Text energyCostText;

    public void UpdateDescription(ObjAbility addon)
    {
        addonName.text = addon.GetName();
        description.text = addon.GetDescription();
        cooldownText.text = $"Cooldown: {addon.GetCooldown()} seconds";
        energyCostText.text = $"Energy Cost: {addon.GetEnergyCost()}";
    }
}
