using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float waitTime;
    [SerializeField] private pooler bulletPool;

    void Start()
    {
        bulletPool = GameObject.Find("EnemyBulletPooler").GetComponent<pooler>();
        StartCoroutine(FireBullet());
    }

    private IEnumerator FireBullet()
    {
        GameObject g = bulletPool.GetObject();
        g.transform.position = spawnPoint.position;
        g.transform.rotation = spawnPoint.rotation;
        g.SetActive(true);
        // Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(FireBullet());
    }


}
