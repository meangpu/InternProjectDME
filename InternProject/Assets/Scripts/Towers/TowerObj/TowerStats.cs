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
    [SerializeField] TowerAI towerAiScript;

    private int minDamage;
    private int maxDamage;
    private float range;
    private float rateOfFire;
    private float bulletSpeed;
    private float bulletLifetime;
    private float areaOfDamage;
    private Sprite bulletSprite;
    private int price;
    private bool isLaserType = false;

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
        price = tower.GetUpgradeCost()[towerLevel];
        areaOfDamage = tower.GetAreaOfDamage();
        isLaserType = tower.GetIsLaserType();
        RefreshTowerVisualRange();
    }

    public int GetSellPrice()
    {
        float sellFactor = 0.7f;
        int _sellPrice = 0;

        for (int i = 0; i < towerLevel+1; i++)
        {
            _sellPrice += tower.GetUpgradeCost()[i];
        }

        _sellPrice = Mathf.RoundToInt(_sellPrice*sellFactor);
        return _sellPrice;
    }

    public void LevelUp()
    {
        if(towerLevel < 2)
        {
            towerLevel++;
            GetAllStats();
            towerAiScript.SetupTower();
        }
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
        towerRangeDisplay.transform.localScale = 2 * range * Vector3.one;
    }

    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public float GetAttackRange() => range;
    public float GetRateOfFire() => rateOfFire;
    public float GetBulletSpeed() => bulletSpeed;
    public float GetBulletLifetime() => bulletLifetime;
    public Sprite GetBulletSprite() => bulletSprite;
    public int GetPrice() => price;
    public int GetTowerLevel() => towerLevel;
    public bool GetIsLaserType() => isLaserType;
    public float GetAreaOfDamage() => areaOfDamage;
    public ObjTower GetTowerType() => tower;

    public int NextLVDmg()
    {
        if (towerLevel < 2)
        {
            return tower.GetMaxDamage()[towerLevel+1];
        }
        return tower.GetMaxDamage()[towerLevel];
        
    }

    public float NextLVRange()
    {
        if (towerLevel < 2)
        {
            return tower.GetAttackRange()[towerLevel+1];
        }
        return tower.GetAttackRange()[towerLevel];
    }

}
