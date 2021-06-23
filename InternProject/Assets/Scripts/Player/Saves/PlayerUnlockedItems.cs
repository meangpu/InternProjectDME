using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Unlocked Items", menuName = "Saves/Create Progression Data")]
public class PlayerUnlockedItems : ScriptableObject
{
    [SerializeField] private List<ObjPlayerTank> unlockedTanks = null;
    [SerializeField] private List<ObjTankTurret> unlockedTurrets = null;
    [SerializeField] private List<ObjAbility> unlockedAbilities = null;

    public List<ObjPlayerTank> GetUnlockedTanksList() => unlockedTanks;
    public List<ObjTankTurret> GetUnlockedTurretsList() => unlockedTurrets;
    public List<ObjAbility> GetUnlockedAbilitiessList() => unlockedAbilities;
}
