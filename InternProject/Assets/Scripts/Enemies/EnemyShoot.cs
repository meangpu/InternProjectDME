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
        PoolingSingleton.Instance.EnemyBulletPool.SpawnBullet(spawnPoint.position, spawnPoint.rotation, DealDamage(), 0f);
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
