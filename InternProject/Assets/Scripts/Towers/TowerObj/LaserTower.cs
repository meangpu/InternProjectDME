using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour, ITower
{
    [SerializeField] private TowerStats towerStats = null;
    [SerializeField] private LineRenderer lineRenderer = null;
    [SerializeField] private TowerAI towerAI = null;
    [SerializeField] private Transform laserSpawnPoint = null;

    private Enemy target;

    private void Awake()
    {
        DisableLaser();
    }

    private void Start()
    {
        towerAI.OnNoTargetFoundForLaser += DisableLaser;
        towerAI.OnLaserDamageDealt += DealDamage;
    }

    public void Shoot()
    {
        target = towerAI.GetTarget();
        EnableLaser();
        UpdateLaser();
    }

    private void DealDamage()
    {
        target.TakeDamage(towerStats.DealDamage());
    }

    private void EnableLaser()
    {   
        if (lineRenderer.enabled) { return; }

        lineRenderer.enabled = true;
    }

    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, laserSpawnPoint.position);

        lineRenderer.SetPosition(1, target.GetTransform().position);
    }

    private void DisableLaser()
    {
        if (!lineRenderer.enabled) { return; }

        lineRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        towerAI.OnNoTargetFoundForLaser -= DisableLaser;
    }
}
