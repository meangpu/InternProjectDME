using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 1", menuName = "Levels/Create New Level")]
public class ObjLevel : ScriptableObject
{
    [SerializeField] private int currentStars = 0;
    [TextArea(4, 6)][SerializeField] private string levelDescription = null;
    [SerializeField] private Sprite levelPreview = null;
    [SerializeField] private int startingGold = 0;

    public const int MAX_STARS = 3;

    public int GetCurrentStars() => currentStars;
    public void SetCurrentStars(int value)
    {
        int diff = value - currentStars;

        // Add to star save
        currentStars = value;
    }
    public string GetLevelDescription() => levelDescription;
    public Sprite GetLevelPreview() => levelPreview;
    public int GetStartingGold() => startingGold;
}
