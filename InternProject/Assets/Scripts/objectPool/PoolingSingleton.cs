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
