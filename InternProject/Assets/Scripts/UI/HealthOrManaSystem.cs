using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrManaSystem
{
    public event Action<int, int> OnDamaged;
    public event Action<int, int> OnHealed;

    public event Action OnDeath;

    private int pointAmount;
    private int pointAmountMax;

    public HealthOrManaSystem(int amount)
    {
        pointAmountMax = amount;
        pointAmount = amount;
    }

    public void Damage(int pointsInflicted)
    {
        pointAmount -= pointsInflicted;
        if (pointAmount <= 0)
        {
            pointAmount = 0;

            OnDeath?.Invoke();
        }

        OnDamaged?.Invoke(pointAmount, pointAmountMax);
    }

    public void Heal(int pointsHealed)
    {
        pointAmount += pointsHealed;
        if (pointAmount > pointAmountMax)
        {
            pointAmount = pointAmountMax;
        }

        OnHealed?.Invoke(pointAmount, pointAmountMax);
    }

    public float GetPercentage()
    {
        return (float)pointAmount / pointAmountMax;
    }
}
