using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyDisplay enemy = null;

    private float timeCounter = 0f;
    private float waitTime;
    private bool canShoot = false;

    private void OnEnable()
    {
        waitTime = 1 / enemy.AtkSpeed;
    }

    private void Update()
    {
        if (!canShoot) { return; }

        timeCounter += Time.deltaTime;

        if (timeCounter > waitTime)
        {
            PoolingSingleton.Instance.EnemyBulletPool.SpawnEnemyBullet(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime, enemy.BulletType);
            timeCounter = 0f;
        }
    }

    public void StartShooting()
    {
        canShoot = true;
    }

    public void StopShooting()
    {
        canShoot = false;
    }
}
