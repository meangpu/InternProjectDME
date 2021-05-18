using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform projectileSpawnPointTransform = null;
    [SerializeField] private float targetScanInterval = 0.1f;
    [SerializeField] private float shootInterval = 0.5f;

    [SerializeField] private float range = 5f;

    private Vector3 projectileSpawnPoint;
    private Quaternion projectileRotation;
    private float timeScanPassed;
    private float timeAfterShot;

    private Enemy target;

    private void Update()
    {
        CountTimer();

        if (timeScanPassed >= targetScanInterval)
        {
            FindTarget();
            ResetScanTimer();
        }

        // If target is not null, Shoot at it
        if (target != null && (timeAfterShot >= shootInterval))
        {
            Shoot();
            ResetShootTimer();
        }
    }

    private void Shoot()
    {
        projectileSpawnPoint = projectileSpawnPointTransform.position;
        projectileRotation = projectileSpawnPointTransform.rotation;

        Instantiate(projectilePrefab, projectileSpawnPoint, projectileRotation);
    }

    private void FindTarget()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliderArray)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                SetTarget(enemy);
                break;
            } else
            {
                SetTarget(null);
            }
        }
    }

    private void SetTarget(Enemy enemy)
    {
        target = enemy;
    }

    private void CountTimer()
    {
        float timePassed = Time.deltaTime;
        timeScanPassed += timePassed;
        timeAfterShot += timePassed;
    }

    private void ResetScanTimer()
    {
        timeScanPassed = 0f;
    }

    private void ResetShootTimer()
    {
        timeAfterShot = 0f;
    }

    public Enemy GetTarget() => target;
}
