using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPointTransform = null;
    [SerializeField] private float targetScanInterval = 0.1f;

    private Vector3 projectileSpawnPoint;
    private Quaternion projectileRotation;
    private float timeScanPassed;
    private float timeAfterShot;
    private float shootInterval;
    private float range;

    private TowerStats towerStats;
    private Enemy target;

    private void Awake()
    {
        towerStats = GetComponent<TowerStats>();
    }

    private void OnEnable()
    {
        range = towerStats.GetAttackRange();
        shootInterval = towerStats.GetRateOfFire();
    }

    private void Update()
    {
        CountTimer();

        if (target != null && (timeAfterShot >= shootInterval))
        {
            Shoot();
            ResetShootTimer();
        }

        if (timeScanPassed >= targetScanInterval)
        {
            FindTarget();
            ResetScanTimer();
        } 
    }

    private void Shoot()
    {
        projectileSpawnPoint = projectileSpawnPointTransform.position;
        projectileRotation = projectileSpawnPointTransform.rotation;

        PoolingSingleton.Instance.TowerBulletPool.SpawnObject(projectileSpawnPoint, projectileRotation);
    }

    private void FindTarget()
    {
        if (target != null && target.gameObject.activeSelf && Vector2.Distance(transform.position, target.transform.position) <= range)
        {
            return;
        }

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
