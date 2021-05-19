using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    // Temporary variables
    [SerializeField] private float bulletSpeed = 10f;
    public int damage = 10;
    public float lifeTime = 4f;

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyOverTime());
    }

    private IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

    public void DestroySelf()
    {
        PoolingSingleton.Instance.PlayerBulletPool.ReturnObject(gameObject);
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }
}
