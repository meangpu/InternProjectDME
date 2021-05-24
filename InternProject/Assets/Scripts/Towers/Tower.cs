using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower/Create New Tower")]
public class Tower : ScriptableObject
{
    [SerializeField] private string towerName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite towerBaseSprite;
    [SerializeField] private Sprite projectileSprite;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private bool laserType;

    public string GetName() => towerName;
    public Sprite GetTowerSprite() => sprite;
    public Sprite GetBaseSprite() => towerBaseSprite;
    public Sprite GetProjectileSprite() => projectileSprite;
    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public float GetAttackRange() => attackRange;
    public bool GetIsLaserType() => laserType;
}
