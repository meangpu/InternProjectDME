using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerimeterDefense : MonoBehaviour, ITower
{
    [SerializeField] private TowerStats towerStats = null;
    [SerializeField] private Transform projectileSpawnPoint = null;

    public UnityEvent OnShoot;

    public void Shoot()
    {
        OnShoot?.Invoke();
        PoolingSingleton.Instance.TowerBulletPool.SpawnTowerBullet(projectileSpawnPoint.position, projectileSpawnPoint.rotation, towerStats.DealDamage(), towerStats.GetBulletSpeed(), towerStats.GetBulletLifetime());
    }
}
