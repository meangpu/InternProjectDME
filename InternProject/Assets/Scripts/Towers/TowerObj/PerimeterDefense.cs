using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerimeterDefense : MonoBehaviour, ITower
{
    [SerializeField] private TowerStats towerStats = null;
    [SerializeField] private Transform projectileSpawnPoint = null;

    public void Shoot()
    {
        PoolingSingleton.Instance.TowerBulletPool.SpawnTowerBullet(projectileSpawnPoint.position, projectileSpawnPoint.rotation, towerStats.DealDamage(), towerStats.GetBulletSpeed(), towerStats.GetBulletLifetime());
    }
}
