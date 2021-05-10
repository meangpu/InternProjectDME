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
        StartCoroutine(fireBullet());
    }

    private IEnumerator fireBullet()
    {
        Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(fireBullet());
    }


}
