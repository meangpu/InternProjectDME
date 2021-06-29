using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 1", menuName = "Levels/Create New Level")]
public class ObjLevel : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private int currentStars = 0;
    [TextArea(4, 6)][SerializeField] private string levelDescription = null;
    [SerializeField] private Sprite levelPreview = null;
    [SerializeField] private int startingGold = 0;
    [SerializeField] private int sceneIndex;
    [SerializeField] bool isUnlocked;

    public const int MAX_STARS = 3;

    public int GetCurrentStars() => currentStars;

    public int SetCurrentStars(int value)
    {
        int diff = value - currentStars;
        Debug.Log($"value = {value} /currentStars {currentStars} / diff = {diff}");

        // Add to star save
        currentStars = value;
        return diff;
    }

    public void UnlockThisLevel()
    {
        isUnlocked = true;
    }



    public string GetLevelDescription() => levelDescription;
    public Sprite GetLevelPreview() => levelPreview;
    public int GetStartingGold() => startingGold;
    public int GetSceneIndex() => sceneIndex;
    public string GetLevelName() => levelName;
    public bool GetIsUnlock() => isUnlocked;
}
