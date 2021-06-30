using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour, ITower
{
    [SerializeField] private AssignSound assignSound = null;

    [SerializeField] private TowerStats towerStats = null;
    [SerializeField] private LineRenderer lineRenderer = null;
    [SerializeField] private TowerAI towerAI = null;
    [SerializeField] private Transform laserSpawnPoint = null;

    [SerializeField] private GameObject startVFX = null;
    [SerializeField] private GameObject endVFX = null;

    private Enemy target;
    private readonly List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Awake()
    {   
        FillLists();
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
        assignSound.PlaySound();

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
    }

    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, (Vector2)laserSpawnPoint.position);
        startVFX.transform.SetPositionAndRotation((Vector2)laserSpawnPoint.position, laserSpawnPoint.rotation);

        lineRenderer.SetPosition(1, (Vector2)target.GetTransform().position);
        endVFX.transform.position = lineRenderer.GetPosition(1);
        startVFX.transform.rotation = Quaternion.Inverse(laserSpawnPoint.rotation);
    }

    private void DisableLaser()
    {
        if (!lineRenderer.enabled) { return; }

        lineRenderer.enabled = false;
        assignSound.StopSound();

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }

    private void FillLists()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
            ParticleSystem particle = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (particle != null)
            {
                particles.Add(particle);
            }
        }

        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            ParticleSystem particle = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (particle != null)
            {
                particles.Add(particle);
            }
        }
    }

    private void OnDestroy()
    {
        towerAI.OnNoTargetFoundForLaser -= DisableLaser;
    }
}
