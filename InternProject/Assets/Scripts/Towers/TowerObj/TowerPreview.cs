using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    [SerializeField] private int towerLevel = 0;
    [SerializeField] private Sprite[] levelDisplayIcon = new Sprite[3];
    [SerializeField] private ObjTower tower = null;
    [SerializeField] private SpriteRenderer towerBase = null;
    [SerializeField] private SpriteRenderer towerTurret = null;
    [SerializeField] private SpriteRenderer towerLevelDisplay = null;
    [SerializeField] GameObject towerRangeDisplay;
    [SerializeField] GameObject NewTowerRangeDisplay;
    [SerializeField] Transform mainTowerTrans;

    private int minDamage;
    private int maxDamage;
    private float range;
    private float rateOfFire;
    private float bulletSpeed;
    private float bulletLifetime;
    private Sprite bulletSprite;

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
        NewTowerRangeDisplay.SetActive(false);
        RefreshTowerVisualRange();
    }

    public void SetTowerType(ObjTower tower)
    {
        this.tower = tower;
        GetAllStats();
    }

    public void SetTowerTypeToMainTower()
    {
        ObjTower _mainTowerObj = mainTowerTrans.GetChild(0).GetComponent<TowerStats>().GetTowerType();
        SetTowerType(_mainTowerObj);
    }

    public void LevelUp()
    {
        if(towerLevel < 2)
        {
            towerLevel++;
            GetAllStats();
        }
    }

    public void resetLevel()
    {
        towerLevel = 0;
    }

    public void RefreshTowerVisualRange()
    {
        // for some reason rane is 2 times bigger
        towerRangeDisplay.transform.localScale = Vector3.one * range *2;
    }

    public void showNextLevelRange()
    {
        NewTowerRangeDisplay.SetActive(true);
        if (towerLevel <= 1)
        {
            NewTowerRangeDisplay.transform.localScale = Vector3.one * tower.GetAttackRange()[towerLevel+1] *2;
        }
    }

    public void ShowOnlyRange()
    {
        gameObject.SetActive(true);
        NewTowerRangeDisplay.SetActive(false);
        towerRangeDisplay.SetActive(true);
        disableTowerImage();
    }

    public void showTowerImage()
    {
        towerBase.gameObject.SetActive(true);
        towerTurret.gameObject.SetActive(true);
        towerLevelDisplay.gameObject.SetActive(true);
    }

    public void disableTowerImage()
    {
        towerBase.gameObject.SetActive(false);
        towerTurret.gameObject.SetActive(false);
        towerLevelDisplay.gameObject.SetActive(false);
    }


}
