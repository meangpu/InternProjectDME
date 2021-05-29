using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Equipped Addons", menuName = "Saves/Create Player Equipped Addons")]
public class PlayerEquippedAddons : ScriptableObject
{
    [SerializeField] private ObjAbility abilityQ;
    [SerializeField] private ObjAbility abilityE;

    private List<ObjAbility> equippedAddons;

    private void Awake()
    {
        equippedAddons = new List<ObjAbility>();

        equippedAddons.Add(abilityQ);
        equippedAddons.Add(abilityE);
    }

    public void SetAbilityQ(ObjAbility ability)
    {
        AbilityClashCheck(ability, abilityQ);
        abilityQ = ability;  
    }

    private void AbilityClashCheck(ObjAbility abilityToApply, ObjAbility abilitySlot) // If the same abilities is equipped twice, swap them.
    {
        if (abilityQ.GetAbilityType() == AbilityType.Empty) { return; } // If one of it is empty, ignore. So 2 empty slots won't swap places

        if (abilityToApply == abilitySlot)
        {

        }
    }
}
