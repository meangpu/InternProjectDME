using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Equipped Addons", menuName = "Saves/Create Player Equipped Addons")]
public class PlayerEquippedAddons : ScriptableObject
{
    [SerializeField] private List<ObjAbility> equippedAddons;

    public event Action<int> OnUpdateAddon;

    public enum AddonSlot
    {
        SlotQ,
        SlotE
    }

    public void SetAbility(ObjAbility ability, AddonSlot slot)
    {
        int slotIndex = (int)slot;
        AbilityClashCheck(ability, slotIndex, 1 - slotIndex);
        equippedAddons[slotIndex] = ability;
        OnUpdateAddon?.Invoke(slotIndex);
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
        OnUpdateAddon?.Invoke(slotIndexB);
    }

    public List<ObjAbility> GetEquippedAddons() => equippedAddons;
}
