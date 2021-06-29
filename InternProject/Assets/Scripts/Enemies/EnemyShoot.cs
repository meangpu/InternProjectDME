using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private EnemyDisplay enemy = null;
    [SerializeField] private AmmoType ammoType = AmmoType.Default;

    private PoolingSingleton pooler;
    private bool shootFirst = false;

    public enum AmmoType
    {
        Default,
        HomingMissile,
        Bomb,
        Alternate
    }

    private float timeCounter = 0f;
    private float waitTime;
    private bool canShoot = false;

    private void Start()
    {
        pooler = PoolingSingleton.Instance;

        Setup();

        switch (ammoType)
        {
            default:
                return;
            case AmmoType.Alternate:
                canShoot = true;
                return;
        } 
    }

    private void OnEnable()
    {
        Setup();
    }

    private void Update()
    {
        timeCounter = Mathf.Min(timeCounter + Time.deltaTime, waitTime);

        if (!canShoot) { return; }

        if (timeCounter < waitTime) { return; }

        timeCounter -= waitTime;

        switch (ammoType)
        {
            default:
                pooler.EnemyBulletPool.SpawnEnemyBullet(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime, enemy.BulletType);
                return;

            case AmmoType.HomingMissile:
                pooler.EnemyMissilePool.SpawnEnemyMissile(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime);
                return;

            case AmmoType.Bomb:
                pooler.EnemyBombPool.SpawnEnemyBomb(spawnPoint.position, spawnPoint.rotation, enemy.MinDamage, enemy.MaxDamage, enemy.AttackRange, enemy.BulletLifetime);
                return;

            case AmmoType.Alternate:
                switch (shootFirst)
                {
                    case true:
                        pooler.EnemyBulletPool.SpawnEnemyBullet(spawnPoint.position, spawnPoint.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime, enemy.BulletType);
                        break;
                    case false:
                        pooler.EnemyBulletPool.SpawnEnemyBullet(spawnPoint2.position, spawnPoint2.rotation, enemy.Damage, enemy.BulletSpeed, enemy.BulletLifetime, enemy.BulletType);
                        break;
                }
                shootFirst = !shootFirst;
                return;
        }

    }

    private void Setup()
    {
        waitTime = 1 / enemy.AtkSpeed;
        timeCounter = 0f;
    }

    public void StartShooting()
    {
        canShoot = true;
    }

    public void StopShooting()
    {
        switch (ammoType)
        {
            default:
                canShoot = false;
                return;
            case AmmoType.Alternate:
                return;
        } 
    }
}
