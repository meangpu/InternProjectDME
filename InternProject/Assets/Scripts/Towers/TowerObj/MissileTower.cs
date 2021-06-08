using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTower : MonoBehaviour, ITower
{
    [SerializeField] private TowerStats towerStats = null;
    [SerializeField] private Transform projectileSpawnPoint = null;

    public void Shoot()
    {
        PoolingSingleton.Instance.TowerMissilePool.SpawnTowerMissile(
            projectileSpawnPoint.position, 
            projectileSpawnPoint.rotation, 
            towerStats.DealDamage(), 
            towerStats.GetBulletSpeed(), 
            towerStats.GetAreaOfDamage(), 
            towerStats.GetBulletLifetime());
    }
}
