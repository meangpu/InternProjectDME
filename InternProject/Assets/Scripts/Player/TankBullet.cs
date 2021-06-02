using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody2D rb = null;

    private int damage;
    private float knockback;
    private float lifetime;
    private float bulletSpeed;

    public int Damage { get => damage; set => damage = value; }
    public float KnockBack { get => knockback; set => knockback = value; }
    public float Lifetime { get => lifetime; set => lifetime = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    public Rigidbody2D GetRB() => rb;


    private void OnEnable()
    {
        Move();
        StartCoroutine(DestroyOverTime());
    }

    private IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(lifetime);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemy.TakeDamage(damage);
            enemy.TakeKnockback(transform.position, knockback);
            DestroySelf();
        }
    }
}
