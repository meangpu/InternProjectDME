using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour, IProjectile, IAreaOfDamage
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private bool dealsAOEDamage = false;

    private int damage;
    private float knockback;
    private float lifetime;
    private float bulletSpeed;

    private float areaOfDamage;

    private bool isActivated = false;

    public int Damage { get => damage; set => damage = value; }
    public float KnockBack { get => knockback; set => knockback = value; }
    public float Lifetime { get => lifetime; set => lifetime = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float AreaOfDamage { get => areaOfDamage; set => areaOfDamage = value; }

    public Rigidbody2D GetRB() => rb;


    private void OnEnable()
    {
        isActivated = false;
        Move();
        StartCoroutine(DestroyOverTime());
    }

    private void Update()
    {
        if (!dealsAOEDamage) { return; }

        Move();
    }

    private IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(lifetime);
        DestroySelf();
    }

    public void DestroySelf()
    {
        if (!dealsAOEDamage)
        {
            PoolingSingleton.Instance.PlayerBulletPool.ReturnObject(gameObject);
        }
        else
        {
            PoolingSingleton.Instance.HomingMissilePool.ReturnObject(gameObject);
        }
        
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated) { return; }

        GameObject collidingObject = collision.gameObject;

        if (collidingObject.TryGetComponent(out IEnemy enemy))
        {
            if (dealsAOEDamage)
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(collidingObject.transform.position, areaOfDamage);

                foreach (Collider2D collider in enemies)
                {
                    if (collider.TryGetComponent(out IEnemy enemyInRange))
                    {
                        enemyInRange.TakeDamage(damage);
                    }
                }
            }
            else
            {
                enemy.TakeDamage(damage);
                enemy.TakeKnockback(transform.position, knockback);
            }

            isActivated = true;
            DestroySelf();
        }
    }
}
