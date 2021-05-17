using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    private Pooler pool;

    // Temporary variables
    [SerializeField] private float bulletSpeed = 10f;
    public int damage = 10;
    public float lifeTime = 4f;

    private void Start()
    {
        pool = transform.parent.GetComponent<Pooler>();
    }

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
        pool.ReturnObject(gameObject);
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }
}
