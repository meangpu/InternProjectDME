using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObjTower", menuName = "ObjTower/Create New ObjTower")]
public class ObjTower : ScriptableObject
{
    [SerializeField] private string towerName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite towerBaseSprite;
    [SerializeField] private Sprite projectileSprite;
    [SerializeField] private int[] minDamage;
    [SerializeField] private int[] maxDamage;
    [SerializeField] private float[] attackRange;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float areaOfDamage;
    [SerializeField] private int[] upgradeCost;
    [SerializeField] private bool laserType;

    public string GetName() => towerName;
    public Sprite GetTowerSprite() => sprite;
    public Sprite GetBaseSprite() => towerBaseSprite;
    public Sprite GetProjectileSprite() => projectileSprite;
    public int[] GetMinDamage() => minDamage;
    public int[] GetMaxDamage() => maxDamage;
    public float[] GetAttackRange() => attackRange;
    public float GetRateOfFire() => rateOfFire;
    public float GetProjectileLifeTime() => projectileLifetime;
    public float GetProjectileSpeed() => projectileSpeed;
    public float GetAreaOfDamage() => areaOfDamage;
    public bool IsLaserType() => laserType;
    public int[] GetUpgradeCost() => upgradeCost;
}
