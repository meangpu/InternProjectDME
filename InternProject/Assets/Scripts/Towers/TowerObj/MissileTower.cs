using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissileTower : MonoBehaviour, ITower
{
    [SerializeField] private TowerStats towerStats = null;
    [SerializeField] private Transform projectileSpawnPoint = null;

    public UnityEvent OnShoot;

    public void Shoot()
    {
        PoolingSingleton.Instance.TowerMissilePool.SpawnTowerMissile(
            projectileSpawnPoint.position, 
            projectileSpawnPoint.rotation, 
            towerStats.GetMinDamage(),
            towerStats.GetMaxDamage(),
            towerStats.GetBulletSpeed(), 
            towerStats.GetAreaOfDamage(), 
            towerStats.GetBulletLifetime());
        
        OnShoot?.Invoke();
    }
}
