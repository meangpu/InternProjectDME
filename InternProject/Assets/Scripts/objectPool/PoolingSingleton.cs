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