using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = null;

    private Vector3 targetPosition;

    private void Create(Vector3 position, Vector3 targetPosition)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        Transform projectileTransform = projectile.transform;

        TowerProjectile towerProjectile = projectileTransform.GetComponent<TowerProjectile>();
        towerProjectile.Setup(targetPosition);
    }

    private void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
