using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private DamageSetter damageSetter = null;

    // Temporary variables
    [SerializeField] private float bulletSpeed = 10f;
    public float lifeTime = 4f;

    private int damage;

    public int Damage { get { return damage; } }

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        damage = damageSetter.Damage;
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
