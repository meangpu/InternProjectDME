using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Progression Data", menuName = "Saves/Create Level Progression")]
public class LevelProgression : ScriptableObject
{
    [SerializeField] private ObjLevel[] levels;
}
