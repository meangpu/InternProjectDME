using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float waitTime;
    [SerializeField] private float startWaitTime = 2f;
    [SerializeField] private EnemyDisplay enemy = null;

    private IEnumerator countFirebullet;

    public void StartShoot()
    {
        waitTime = 1/enemy.AtkSpeed;
        if (countFirebullet != null)
        {
            // this make sure that only one Coroutine count
            StopCoroutine(countFirebullet);
        }
        // countFirebullet = FireBullet();
        // StartCoroutine(countFirebullet);

        StartCoroutine(FirstTimeWait());
    }

    private IEnumerator FireBullet()
    {
        PoolingSingleton.Instance.EnemyBulletPool.SpawnEnemyBullet(spawnPoint.position, spawnPoint.rotation, DealDamage(), 1, 1, enemy.BulletType);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(FireBullet());
    }

    private IEnumerator FirstTimeWait()
    {
        yield return new WaitForSeconds(startWaitTime);
        StartCoroutine(FireBullet());
    }

    private int DealDamage()
    {
        return Random.Range(enemy.MinDamage, enemy.MaxDamage + 1);
    }
}
