using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private DamageSetter damageSetter = null;

    // Temporary variables
    public float bulletSpeed = 10f;
    public float lifeTime = 4f;
    private float knockbackForce = 0.1f;

    private int damage;
    public float knockBack;

    public int Damage { get { return damage; } }
    public Rigidbody2D GetRB() => rb;


    private void OnEnable()
    {
        Move();
        damage = damageSetter.Damage;
        knockBack = damageSetter.KnockBack;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemy.TakeDamage(damage);
            enemy.TakeKnockback(transform.position, knockbackForce);
            DestroySelf();
        }
    }
}
