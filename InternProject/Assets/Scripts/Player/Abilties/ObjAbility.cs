using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Addon", menuName = "Addons/Create New Addons")]
public class ObjAbility : ScriptableObject
{
    [SerializeField] private string addonName;
    [TextArea(4,6)] [SerializeField] private string description;
    [SerializeField] private AbilityType abilityID;
    [SerializeField] private int energyCost;
    [SerializeField] private float cooldownTime;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private List<ObjAbility> comboList;
    [SerializeField] private IAbility abilityScript;

    public string GetName() => addonName;
    public string GetDescription() => description;
    public AbilityType GetAbilityType() => abilityID;
    public float GetCooldown() => cooldownTime;
    public int GetDamage() => damage;
    public int GetEnergyCost() => energyCost;
    public float GetRange() => range;
    public IAbility GetRef() => abilityScript;

}
