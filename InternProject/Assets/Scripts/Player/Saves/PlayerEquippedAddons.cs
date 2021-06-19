using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Equipped Addons", menuName = "Saves/Create Player Equipped Addons")]
public class PlayerEquippedAddons : ScriptableObject
{
    [SerializeField] private List<ObjAbility> equippedAddons;
    [SerializeField] private ObjAbility emptyAbility;

    private ComboType comboType = ComboType.None;

    public event Action<int, bool> OnUpdateAddon;

    private void OnEnable()
    {
        ////// prevent data reset across scene
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
    

    public enum AddonSlot
    {
        SlotQ,
        SlotE
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
        OnUpdateAddon?.Invoke(slotIndex, CheckForCombo(slotIndex, otherSlotIndex));
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
        AbilityType thisAbilityType = thisSlot.GetAbilityType();
        AbilityType otherAbilityType = otherSlot.GetAbilityType();

        for (int i = 0; i < comboList.Count; i++)
        {
            if (otherAbilityType == comboList[i].GetAbilityType())
            {
                switch (otherAbilityType)
                {
                    case AbilityType.Bomb:
                        switch (thisAbilityType)
                        {
                            case AbilityType.HomingMissile:
                                comboType = ComboType.UpgradedMissile;
                                break;
                            case AbilityType.Electrocharge:
                                comboType = ComboType.EMP;
                                break;
                        }
                        break;
                    case AbilityType.Dash:
                        comboType = ComboType.EnergyDash;
                        break;
                    case AbilityType.EnergyShield:
                        switch (thisAbilityType)
                        {
                            case AbilityType.Dash:
                                comboType = ComboType.EnergyDash;
                                break;
                            case AbilityType.EnergyOrb:
                                comboType = ComboType.EnergyDrainOrb;
                                break;
                        }
                        break;
                    case AbilityType.Electrocharge:
                        switch (thisAbilityType)
                        {
                            case AbilityType.Bomb:
                                comboType = ComboType.EMP;
                                break;
                            case AbilityType.IncendiaryAmmo:
                                comboType = ComboType.IncendiaryCharge;
                                break;
                        }
                        break;
                    case AbilityType.IncendiaryAmmo:
                        comboType = ComboType.IncendiaryCharge;
                        break;
                    case AbilityType.HomingMissile:
                        comboType = ComboType.UpgradedMissile;
                        break;
                    case AbilityType.EnergyOrb:
                        comboType = ComboType.EnergyDrainOrb;
                        break;
                }
                return true;
            }
        }

        comboType = ComboType.None;
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

    public ComboType GetComboType() => comboType;
}
