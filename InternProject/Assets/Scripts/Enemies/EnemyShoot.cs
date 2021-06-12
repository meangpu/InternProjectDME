using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyDisplay enemy = null;
    [SerializeField] private AmmoType ammoType = AmmoType.Default;

    private PoolingSingleton pooler;

    public enum AmmoType
    {
        Default,
        HomingMissile,
        Bomb
    }

    private float timeCounter = 0f;
    private float waitTime;
    private bool canShoot = false;

    private void Start()
    {
        pooler = PoolingSingleton.Instance;
    }

    private void OnEnable()
    {
        waitTime = 1 / enemy.AtkSpeed;
    }

    private void Update()
    {
        timeCounter = Mathf.Min(timeCounter + Time.deltaTime, waitTime);

        if (!canShoot) { return; }

        if (timeCounter >= waitTime)
        {   
            timeCounter = 0f;

            switch (ammoType)
            {
                default:
                    pooler.EnemyBulletPool.SpawnEnemyBullet(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime, enemy.BulletType);
                    return;

                case AmmoType.HomingMissile:
                    pooler.EnemyMissilePool.SpawnEnemyMissile(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime);
                    return;

                case AmmoType.Bomb:
                    pooler.EnemyBombPool.SpawnEnemyBomb(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.AttackRange, enemy.BulletLifetime);
                    return;
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
