using System;
using System.Collections.Generic;
using UnityEngine;

public class AddonsSelectionInputManager : MonoBehaviour
{
    [SerializeField] private PlayerEquippedAddons playerAddonsObject = null;
    [SerializeField] private ObjAbility emptyAddon = null;

    private PlayerControls playerControls;

    private ObjAbility abilityToBeAssigned;

    public event Action OnChoose;
    public event Action OnConfirm;
    public event Action OnCancel;

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

    private void HidePanel(bool playSound = true)
    {
        if (playSound)
        {
            OnCancel?.Invoke();
        }

        ClearAbilityToBeAssigned();
        gameObject.SetActive(false);
    }

    public void AssignAbility(PlayerEquippedAddons.AddonSlot slot)
    {
        OnConfirm?.Invoke();
        playerAddonsObject.SetAbility(abilityToBeAssigned, slot);
        HidePanel(false);
    }

    public void PrepareForAbilityAssignment(ObjAbility ability)
    {
        abilityToBeAssigned = ability;
        OnChoose?.Invoke();
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
