using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [SerializeField] private float targetScanInterval = 0.1f;

    private float timeScanPassed;
    private float timeAfterShot;
    private float shootInterval;
    private float range;
    private bool isLaserType;

    private TowerStats towerStats;
    private Enemy target;
    private ITower towerShoot;

    public Action OnNoTargetFoundForLaser;
    public Action OnLaserDamageDealt;

    private void Awake()
    {
        towerStats = GetComponent<TowerStats>();
        towerShoot = GetComponent<ITower>();
    }

    private void Start()
    {
        SetupTower();
    }

    private void OnEnable()
    {
        SetupTower();
    }

    private void Update()
    {
        CountTimer();

        if (timeScanPassed >= targetScanInterval)
        {
            FindTarget();
            ResetScanTimer();
        }

        if (isLaserType)
        {
            if (target != null)
            {
                ShootEnemy();

                if (timeAfterShot >= shootInterval)
                {
                    OnLaserDamageDealt?.Invoke();
                    ResetShootTimer();
                }
            }
            else
            {
                OnNoTargetFoundForLaser?.Invoke();
            }

            return;
        }

        if (target != null && (timeAfterShot >= shootInterval))
        {
            ShootEnemy();
            ResetShootTimer();
        }
    }

    private void ShootEnemy()
    {
        towerShoot.Shoot();
    }

    public void SetupTower()
    {
        range = towerStats.GetAttackRange();
        shootInterval = towerStats.GetRateOfFire();
        isLaserType = towerStats.GetIsLaserType();
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
                ResetShootTimer();
                SetTarget(enemy);
                return;
            } 
            else
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

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (transform.position, range);
    }

    public Enemy GetTarget() => target;
}
