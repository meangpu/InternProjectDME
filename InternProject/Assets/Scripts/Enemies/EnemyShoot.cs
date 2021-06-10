using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyDisplay enemy = null;
    [SerializeField] private bool spawnHomingBullets = false;

    private float timeCounter = 0f;
    private float waitTime;
    private bool canShoot = false;

    private void OnEnable()
    {
        waitTime = 1 / enemy.AtkSpeed;
    }

    private void Update()
    {
        timeCounter = Mathf.Min(timeCounter + Time.deltaTime, waitTime);

        if (!canShoot) { return; }

        if (timeCounter == waitTime)
        {   
            timeCounter = 0f;

            if (spawnHomingBullets)
            {
                PoolingSingleton.Instance.EnemyMissilePool.SpawnEnemyMissile(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime);
            }
            else
            {
                PoolingSingleton.Instance.EnemyBulletPool.SpawnEnemyBullet(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime, enemy.BulletType);
            }   
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
