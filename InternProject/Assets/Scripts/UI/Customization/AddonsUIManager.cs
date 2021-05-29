using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddonsUIManager : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private TMP_Text addonName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private TMP_Text energyCostText;
    [SerializeField] private AddonsSelectionInputManager inputManager;

    private void Start()
    {
        HideDescription();
    }

    public void UpdateDescription(ObjAbility addon)
    {
        ShowDescription();
        addonName.text = addon.GetName();
        description.text = addon.GetDescription();
        cooldownText.text = $"Cooldown: {addon.GetCooldown()} seconds";
        energyCostText.text = $"Energy Cost: {addon.GetEnergyCost()}";
    }

    public void HideDescription()
    {
        parent.SetActive(false);
    }

    public void ShowDescription()
    {
        parent.SetActive(true);
    }

    public void SelectAddonToAssign(ObjAbility ability)
    {
        inputManager.gameObject.SetActive(true);
        HideDescription();
        inputManager.PrepareForAbilityAssignment(ability);
    }
}
