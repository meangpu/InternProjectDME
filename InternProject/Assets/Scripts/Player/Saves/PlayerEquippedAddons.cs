using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquippedAddons : ScriptableObject
{
    private AbilityType abilityQ;
    private AbilityType abilityE;

    private List<AbilityType> equippedAddons;

    private void Awake()
    {
        equippedAddons = new List<AbilityType>();

        equippedAddons.Add(abilityQ);
        equippedAddons.Add(abilityE);
    }

    public void SetAbilityQ(AbilityType ability)
    {
        AbilityClashCheck(ability, abilityQ);
        abilityQ = ability;  
    }

    private void AbilityClashCheck(AbilityType abilityToApply, AbilityType abilitySlot) // If the same abilities is equipped twice, swap them.
    {
        if (abilityQ == AbilityType.Empty) { return; } // If one of it is empty, ignore. So 2 empty slots won't swap places

        if (abilityToApply == abilitySlot)
        {

        }
    }
}
