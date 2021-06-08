using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTower : MonoBehaviour, ITower
{
    [SerializeField] private TowerStats towerStats = null;
    [SerializeField] private Transform projectileSpawnPoint = null;

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
