using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolingSingleton : MonoBehaviour
{
    public static PoolingSingleton Instance { get; private set; }

    [SerializeField] private Pooler enemyBulletPool = null;
    [SerializeField] private Pooler playerBulletPool = null;
    [SerializeField] private Pooler towerBulletPool = null;
    [SerializeField] private Pooler enemyPool = null;
    [SerializeField] private Pooler popUpPool = null;
    [SerializeField] private Pooler goldPool = null;
    [SerializeField] private Pooler homingMissilePool = null;
    [SerializeField] private Pooler enemyDeathPar = null;
    [SerializeField] private Pooler towerMissilePool = null;
    [SerializeField] private Pooler enemyBasicTankPool = null;
    [SerializeField] private Pooler artilleryTankPool = null;
    [SerializeField] private Pooler enemyMissilePool = null;
    [SerializeField] private Pooler doomsdayTankPool = null;
    [SerializeField] private Pooler spyPlanePool = null;
    [SerializeField] private Pooler bomberPlanePool = null;
    [SerializeField] private Pooler bombDropPool = null;
    [SerializeField] private Pooler mWingPool = null;
    [SerializeField] private Pooler collectiblePool = null;
    [SerializeField] private Pooler bulletExplosionPool = null;
    [SerializeField] private Pooler machineExplosionPool = null;
    [SerializeField] private Pooler audioSourcePool = null;

    public Pooler EnemyBulletPool 
    { 
        get { return enemyBulletPool; }
    }
    public Pooler PlayerBulletPool
    {
        get { return playerBulletPool; }
    }
    public Pooler TowerBulletPool
    {
        get { return towerBulletPool; }
    }
    public Pooler EnemyPool
    {
        get { return enemyPool; }
    }
    public Pooler PopUpPool
    {
        get { return popUpPool; }
    }
    public Pooler GoldPool
    {
        get { return goldPool; }
    }
    public Pooler HomingMissilePool
    {
        get { return homingMissilePool; }
    }
    public Pooler ParticlesHumanDeathPool
    {
        get { return enemyDeathPar; }
    }
    public Pooler TowerMissilePool
    {
        get { return towerMissilePool; }
    }
    public Pooler EnemyBasicTankPool
    {
        get { return enemyBasicTankPool; }
    }
    public Pooler EnemyArtilleryTankPool
    {
        get { return artilleryTankPool; }
    }
    public Pooler EnemyMissilePool
    {
        get { return enemyMissilePool; }
    }
    public Pooler EnemyDoomsdayTankPool
    {
        get { return doomsdayTankPool; }
    }
    public Pooler EnemySpyPlanePool
    {
        get { return spyPlanePool; }
    }
    public Pooler EnemyBomberPlanePool
    {
        get { return bomberPlanePool; }
    }
    public Pooler EnemyBombPool
    {
        get { return bombDropPool; }
    }
    public Pooler MWingPool
    {
        get { return mWingPool; }
    }
    public Pooler CollectiblePool
    {
        get { return collectiblePool; }
    }
    public Pooler BulletExplosion
    {
        get { return bulletExplosionPool; }
    }
    public Pooler MachineExplosion
    {
        get { return machineExplosionPool; }
    }
    public Pooler AudioSourcePool
    {
        get { return audioSourcePool; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

}
