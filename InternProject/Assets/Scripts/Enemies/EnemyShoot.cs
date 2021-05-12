using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float waitTime;

    void Start()
    {
        StartCoroutine(FireBullet());
    }

    private IEnumerator FireBullet()
    {
        Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(FireBullet());
    }


}
