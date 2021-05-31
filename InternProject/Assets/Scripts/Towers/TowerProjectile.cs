using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private DamageSetter damageSetter = null;

    [Header("Temp Fields")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float knockbackForce = 0.1f;

    private int damage;

    public int Damage { get { return damage; } }

    private void OnEnable()
    {
        Move();
        damage = damageSetter.Damage;
        StartCoroutine(DestroyOverTme());
    }

    public void DestroySelf()
    {
        PoolingSingleton.Instance.TowerBulletPool.ReturnObject(gameObject);
    }

    private IEnumerator DestroyOverTme()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy enemy))
        {
            // add kb from bullet
            enemy.TakeDamage(damage);
            enemy.TakeKnockback(transform.position, knockbackForce);
            DestroySelf();
        }
    }
}
