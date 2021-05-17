using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : ScriptableObject
{
    [SerializeField] private string towerName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite projectileSprite;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private bool laserType;

    public string GetName() => towerName;
    public Sprite GetTowerSprite() => sprite;
    public Sprite GetProjectileSprite() => projectileSprite;
    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public float GetAttackRange() => attackRange;
    public bool GetIsLaserType() => laserType;

    public int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
}
