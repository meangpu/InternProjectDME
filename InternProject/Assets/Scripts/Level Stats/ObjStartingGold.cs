using UnityEngine;

[CreateAssetMenu(fileName = "Level 1", menuName = "Levels/Create New Start Gold")]
public class ObjStartingGold : ScriptableObject
{
    [SerializeField] private int startingGold = 0;

    public int GetStartingGold() => startingGold;
}
