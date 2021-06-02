using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [SerializeField] private int towerLevel = 1;
    [SerializeField] private ObjTower tower = null;
    [SerializeField] private SpriteRenderer towerBase = null;
    [SerializeField] private SpriteRenderer towerTurret = null;

    private int minDamage;
    private int maxDamage;
    private float range;
    private float rateOfFire;
    private float bulletSpeed;
    private float bulletLifetime;
    private Sprite bulletSprite;

    private void OnEnable()
    {
        towerBase.sprite = tower.GetBaseSprite();
        towerTurret.sprite = tower.GetTowerSprite();

        minDamage = tower.GetMinDamage()[towerLevel - 1];
        maxDamage = tower.GetMaxDamage()[towerLevel - 1];
        range = tower.GetAttackRange()[towerLevel - 1];
        rateOfFire = 1 / tower.GetRateOfFire();
        bulletSpeed = tower.GetProjectileSpeed();
        bulletLifetime = tower.GetProjectileLifeTime();
        bulletSprite = tower.GetProjectileSprite();
    }

    public void SetTowerType(ObjTower tower)
    {
        this.tower = tower;
    }

    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public float GetAttackRange() => range;
    public float GetRateOfFire() => rateOfFire;
    public float GetBulletSpeed() => bulletSpeed;
    public float GetBulletLifetime() => bulletLifetime;
    public Sprite GetBulletSprite() => bulletSprite;
}
