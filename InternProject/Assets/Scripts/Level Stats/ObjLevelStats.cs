using UnityEngine;

[CreateAssetMenu(fileName = "Level 1", menuName = "Levels/Create New Level Progression")]
public class ObjLevelStats : ScriptableObject
{
    [SerializeField] private int currentStars = 0;
    [SerializeField] private bool levelUnlocked = false;

    public int GetCurrentStars() => currentStars;
    public bool IsUnlocked() => levelUnlocked;

    public void SetCurrentStars(int value)
    {
        int diff = value - currentStars;

        if (diff < 0) { return; }

        currentStars += diff;
    }

    public void UnlockThisLevel()
    {
        levelUnlocked = true;
    }
}
