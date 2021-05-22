using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSystem
{
    private int gold;

    public event Action<int> OnGoldUpdated;
    public event Action OnNotEnoughGold;

    public GoldSystem(int startingGold)
    {
        gold = startingGold;
    }

    public bool TrySpendGold(int goldUsed)
    {
        if (gold < goldUsed) 
        {
            OnNotEnoughGold?.Invoke();
            return false; 
        } 
        else
        {
            gold -= goldUsed;

            OnGoldUpdated?.Invoke(gold);
            return true;
        }
    }

    public void AddGold(int goldGained)
    {
        gold += goldGained;

        OnGoldUpdated?.Invoke(gold);
    }

    public int GetGold() => gold;
}
