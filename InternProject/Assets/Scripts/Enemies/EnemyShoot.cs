using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float waitTime;
    [SerializeField] private Pooler bulletPool;

    void Start()
    {
        bulletPool = GameObject.Find("EnemyBulletPooler").GetComponent<Pooler>();
        StartCoroutine(FireBullet());
    }

    private IEnumerator FireBullet()
    {
        bulletPool.SpawnObject(spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(FireBullet());
    }
}
