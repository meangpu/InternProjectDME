using System;

public class GoldSystem
{
    private int ObjGold;

    public event Action<int> OnGoldUpdated;
    public event Action OnNotEnoughGold;

    public GoldSystem(int startingGold)
    {
        ObjGold = startingGold;
    }

    public bool TrySpendGold(int goldUsed)
    {
        if (ObjGold < goldUsed) 
        {
            OnNotEnoughGold?.Invoke();
            return false; 
        } 
        else
        {
            ObjGold -= goldUsed;

            OnGoldUpdated?.Invoke(ObjGold);
            return true;
        }
    }

    public void AddGold(int goldGained)
    {
        ObjGold += goldGained;

        OnGoldUpdated?.Invoke(ObjGold);
    }

    public int GetGold() => ObjGold;
}
