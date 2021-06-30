using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 1", menuName = "Levels/Create New Level")]
public class ObjLevel : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private int levelId;
    [TextArea(4, 6)][SerializeField] private string levelDescription = null;
    [SerializeField] private Sprite levelPreview = null;
    [SerializeField] private int startingGold = 0;
    [SerializeField] private int sceneIndex;
    [SerializeField] private ObjLevel[] unlockedLevels = null;
    [SerializeField] private ObjLevelStats stats;

    public int GetCurrentStars() => stats.GetCurrentStars();

    public int GetStarsDiff(int stars) => Mathf.Max(stars - stats.GetCurrentStars(), 0);

    public void SetCurrentStars(int value)
    {
        stats.SetCurrentStars(value);
    }

    public string GetLevelDescription() => levelDescription;
    public Sprite GetLevelPreview() => levelPreview;
    public int GetStartingGold() => startingGold;
    public int GetSceneIndex() => sceneIndex;
    public string GetLevelName() => levelName;
    public bool GetIsUnlock() => stats.IsUnlocked();
    public ObjLevelStats Stats => stats;
    public int ID => levelId;
    public ObjLevel[] GetUnlockedLevels() => unlockedLevels;
}
