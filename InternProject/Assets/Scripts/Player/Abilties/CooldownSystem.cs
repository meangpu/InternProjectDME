using System.Collections.Generic;
using UnityEngine;

public class CooldownSystem : MonoBehaviour
{
    private readonly List<CooldownData> cooldowns = new List<CooldownData>();

    private void Update()
    {
        ProcessCooldowns();
    }

    public void PutOnCooldown(ObjAbility ability)
    {
        cooldowns.Add(new CooldownData(ability.GetAbilityType(), ability.GetCooldown()));
    }

    public bool IsOnCooldown(AbilityType abilityType)
    {
        foreach(CooldownData cooldown in cooldowns)
        {
            if (cooldown.AbilityType == abilityType) { return true; }    
        }
        
        return false;
    }

    public float GetRemainingDuration(AbilityType abilityType)
    {
        foreach (CooldownData cooldown in cooldowns)
        {
            if (cooldown.AbilityType != abilityType) { continue; }

            return cooldown.RemainingTime;
        }

        return 0f;
    }

    private void ProcessCooldowns()
    {
        float deltaTime = Time.deltaTime;

        for (int i = cooldowns.Count - 1; i >= 0; i--)
        {
            if (cooldowns[i].DecrementCooldown(deltaTime))
            {
                cooldowns.RemoveAt(i);
            }
        }
    }
}

public class CooldownData
{
    public CooldownData(AbilityType abilityType, float cooldownDuration)
    {
        AbilityType = abilityType;
        RemainingTime = cooldownDuration;
    }

    public AbilityType AbilityType { get; }
    public float RemainingTime { get; private set; }

    public bool DecrementCooldown(float deltaTime)
    {
        RemainingTime = Mathf.Max(RemainingTime - deltaTime, 0f);

        return RemainingTime == 0;
    }
}