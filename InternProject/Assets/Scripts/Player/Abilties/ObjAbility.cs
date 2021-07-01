using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Addon", menuName = "Addons/Create New Addons")]
public class ObjAbility : ScriptableObject
{
    [SerializeField] private string addonName;
    [TextArea(4,6)] [SerializeField] private string description;
    [SerializeField] private AbilityType abilityType;
    [SerializeField] private Sprite icon;
    [SerializeField] private int energyCost;
    [SerializeField] private float cooldownTime;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private float range;
    [SerializeField] private float abilityDuration;
    [SerializeField] private float percentage;
    [SerializeField] private List<ObjAbility> comboList;
    [SerializeField] private float comboValue;

    [Header("buy addon")]
    [SerializeField] int buyStarValue;

    public string GetName() => addonName;
    public string GetDescription() => description;
    public AbilityType GetAbilityType() => abilityType;
    public Sprite GetIcon() => icon;
    public float GetCooldown() => cooldownTime;
    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public int GetEnergyCost() => energyCost;
    public float GetRange() => range;
    public float GetDuration() => abilityDuration;
    public float GetPercentage() => percentage;
    public List<ObjAbility> GetComboList() => comboList;
    public float GetComboValue() => comboValue;

    public int GetBuyStarPrice() => buyStarValue;


    private void OnEnable()
    {
        ////// prevent data reset across scene
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }


}
