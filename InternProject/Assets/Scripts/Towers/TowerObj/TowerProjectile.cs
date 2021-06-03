using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody2D rb = null;

    private float bulletSpeed;
    private float lifetime;
    private int damage;
    private bool isActivated;

    public int Damage { get => damage; set => damage = value; }
    public float KnockBack { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Lifetime { get => lifetime; set => lifetime = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public bool IsActivated { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void OnEnable()
    {
        Move();
        StartCoroutine(DestroyOverTme());
    }

    public void DestroySelf()
    {
        PoolingSingleton.Instance.TowerBulletPool.ReturnObject(gameObject);
    }

    private IEnumerator DestroyOverTme()
    {
        yield return new WaitForSeconds(lifetime);
        DestroySelf();
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
            DestroySelf();
        }
    }
}
