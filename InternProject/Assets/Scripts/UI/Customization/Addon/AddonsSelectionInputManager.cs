using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddonsSelectionInputManager : MonoBehaviour
{
    [SerializeField] private PlayerEquippedAddons playerAddonsObject = null;
    [SerializeField] private ObjAbility emptyAddon = null;

    private PlayerControls playerControls;

    private ObjAbility abilityToBeAssigned;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Addons.AssignQ.started += _ => AssignAbility(PlayerEquippedAddons.AddonSlot.SlotQ);
        playerControls.Addons.AssignE.started += _ => AssignAbility(PlayerEquippedAddons.AddonSlot.SlotE);
        playerControls.Addons.Cancel.started += _ => HidePanel();
    }

    private void HidePanel()
    {
        gameObject.SetActive(false);
        ClearAbilityToBeAssigned();
    }

    public void AssignAbility(PlayerEquippedAddons.AddonSlot slot)
    {
        playerAddonsObject.SetAbility(abilityToBeAssigned, slot);
        HidePanel();
    }

    public void PrepareForAbilityAssignment(ObjAbility ability)
    {
        abilityToBeAssigned = ability;
    }

    private void ClearAbilityToBeAssigned()
    {
        abilityToBeAssigned = emptyAddon;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
