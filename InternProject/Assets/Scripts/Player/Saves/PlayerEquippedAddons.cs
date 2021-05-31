using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Equipped Addons", menuName = "Saves/Create Player Equipped Addons")]
public class PlayerEquippedAddons : ScriptableObject
{
    [SerializeField] private List<ObjAbility> equippedAddons;
    [SerializeField] private ObjAbility emptyAbility;

    private bool isCombo;

    public event Action<int, bool> OnUpdateAddon;

    public enum AddonSlot
    {
        SlotQ,
        SlotE
    }

    public enum ComboType
    {
        None,
        EnergyDrainOrb,
        EnergyDash,
        EMP,
        IncendiaryCharge,
        UpgradedMissile
    }

    public void ClearAbility(AddonSlot slot)
    {
        SetAbility(emptyAbility, slot);
    }

    public void SetAbility(ObjAbility ability, AddonSlot slot)
    {
        int slotIndex = (int)slot;
        int otherSlotIndex = 1 - slotIndex;
        AbilityClashCheck(ability, slotIndex, otherSlotIndex);
        equippedAddons[slotIndex] = ability;
        isCombo = CheckForCombo(slotIndex, otherSlotIndex);
        OnUpdateAddon?.Invoke(slotIndex, isCombo);
    }

    private void AbilityClashCheck(ObjAbility abilityToApply, int thisSlotIndex, int otherSlotIndex) // If the same abilities is equipped twice, swap them.
    {
        ObjAbility otherAbilitySlot = equippedAddons[otherSlotIndex];

        if (otherAbilitySlot.GetAbilityType() == AbilityType.Empty) { return; } // If one of it is empty, ignore. So 2 empty slots won't swap places

        if (abilityToApply.GetAbilityType() == otherAbilitySlot.GetAbilityType())
        {
            SwapAbilities(thisSlotIndex, otherSlotIndex);
        }
    }

    private void SwapAbilities(int slotIndexA, int slotIndexB)
    {
        ObjAbility temp = equippedAddons[slotIndexA];
        equippedAddons[slotIndexA] = equippedAddons[slotIndexB];
        equippedAddons[slotIndexB] = temp;
        OnUpdateAddon?.Invoke(slotIndexB, false);
    }

    private bool CheckForCombo(int slotIndex, int otherSlotIndex)
    {   
        ObjAbility thisSlot = equippedAddons[slotIndex];
        ObjAbility otherSlot = equippedAddons[otherSlotIndex];
        List<ObjAbility> comboList = thisSlot.GetComboList();
        

        for (int i = 0; i < comboList.Count; i++)
        {
            if (otherSlot.GetAbilityType() == comboList[i].GetAbilityType())
            {
                return true;
            }
        }

        return false;
    }

    public List<ObjAbility> GetEquippedAddons() => equippedAddons;

    public bool IsEnergyShieldEquipped()
    {
        for (int i = 0; i < equippedAddons.Count; i++)
        {
            if (equippedAddons[i].GetAbilityType() == AbilityType.EnergyShield)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsCombo => isCombo;
}
