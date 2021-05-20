using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float waitTime;
    [SerializeField] private float startWaitTime = 2f;
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
        PoolingSingleton.Instance.EnemyBulletPool.SpawnObject(spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(FireBullet());
    }

    private IEnumerator FirstTimeWait()
    {
        yield return new WaitForSeconds(startWaitTime);
        StartCoroutine(FireBullet());
    }
}
