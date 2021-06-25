using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    private readonly List<AbilityData> abilitiesTimer = new List<AbilityData>();

    public event Action<AbilityType> OnTimerFinished;

    private void Update()
    {
        ProcessTimer();
    }

    public void PutOnTimer(AbilityType ability, float duration)
    {
        abilitiesTimer.Add(new AbilityData(ability, duration));
    }

    public bool IsActivated(AbilityType abilityType)
    {
        foreach (AbilityData cooldown in abilitiesTimer)
        {
            if (cooldown.AbilityType == abilityType) { return true; }
        }

        return false;
    }

    public float GetRemainingDuration(AbilityType abilityType)
    {
        foreach (AbilityData cooldown in abilitiesTimer)
        {
            if (cooldown.AbilityType != abilityType) { continue; }

            return cooldown.RemainingTime;
        }

        return 0f;
    }

    private void ProcessTimer()
    {
        float deltaTime = Time.deltaTime;

        for (int i = abilitiesTimer.Count - 1; i >= 0; i--)
        {
            if (abilitiesTimer[i].DecrementCooldown(deltaTime))
            {
                OnTimerFinished?.Invoke(abilitiesTimer[i].AbilityType);
                abilitiesTimer.RemoveAt(i); 
            }
        }
    }
}

public class AbilityData
{
    public AbilityData(AbilityType abilityType, float duration)
    {
        AbilityType = abilityType;
        RemainingTime = duration;
    }

    public AbilityType AbilityType { get; }

    public float RemainingTime { get; private set; }

    public bool DecrementCooldown(float deltaTime)
    {
        RemainingTime = Mathf.Max(RemainingTime - deltaTime, 0f);

        return RemainingTime == 0;
    }
}
