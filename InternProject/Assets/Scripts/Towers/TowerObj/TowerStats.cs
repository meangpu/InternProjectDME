using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [SerializeField] private int towerLevel = 0;
    [SerializeField] private Sprite[] levelDisplayIcon = new Sprite[3];
    [SerializeField] private ObjTower tower = null;
    [SerializeField] private SpriteRenderer towerBase = null;
    [SerializeField] private SpriteRenderer towerTurret = null;
    [SerializeField] private SpriteRenderer towerLevelDisplay = null;
    [SerializeField] GameObject towerRangeDisplay;

    private int minDamage;
    private int maxDamage;
    private float range;
    private float rateOfFire;
    private float bulletSpeed;
    private float bulletLifetime;
    private Sprite bulletSprite;

    private void Start()
    {
        if (tower != null)
        {
            GetAllStats();
            RefreshTowerVisualRange();
        }
    }

    private void OnEnable()
    {
        if (tower != null)
        {
            GetAllStats();
            GetAllStats();
        }    
    }

    private void GetAllStats()
    {
        towerBase.sprite = tower.GetBaseSprite();
        towerTurret.sprite = tower.GetTowerSprite();
        towerLevelDisplay.sprite = levelDisplayIcon[towerLevel];
        minDamage = tower.GetMinDamage()[towerLevel];
        maxDamage = tower.GetMaxDamage()[towerLevel];
        range = tower.GetAttackRange()[towerLevel];
        rateOfFire = 1 / tower.GetRateOfFire();
        bulletSpeed = tower.GetProjectileSpeed();
        bulletLifetime = tower.GetProjectileLifeTime();
        bulletSprite = tower.GetProjectileSprite();
    }

    public void SetTowerType(ObjTower tower)
    {
        this.tower = tower;
        GetAllStats();
    }

    public int DealDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    public void RefreshTowerVisualRange()
    {
        // for some reason rane is 2 times bigger
        towerRangeDisplay.transform.localScale = Vector3.one * range *2;
    }

    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public float GetAttackRange() => range;
    public float GetRateOfFire() => rateOfFire;
    public float GetBulletSpeed() => bulletSpeed;
    public float GetBulletLifetime() => bulletLifetime;
    public Sprite GetBulletSprite() => bulletSprite;
}
