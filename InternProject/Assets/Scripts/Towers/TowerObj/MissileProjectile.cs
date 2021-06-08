using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour, IAreaOfDamage
{
    [SerializeField] private Rigidbody2D rb = null;

    private float bulletSpeed;
    private float lifetime;
    private int damage;
    private float range;
    private bool isActivated = false;

    public int Damage { get => damage; set => damage = value; }
    public float Lifetime { get => lifetime; set => lifetime = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float AreaOfDamage { get => range; set => range = value; }

    private void OnEnable()
    {
        isActivated = false;
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
        if (isActivated) { return; }

        if (collision.gameObject.TryGetComponent(out Enemy enemyOnContact))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyOnContact.transform.position, range);

            foreach (Collider2D collider in enemies)
            {
                if (collider.TryGetComponent(out IEnemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
            
            isActivated = true;
            DestroySelf();
        }
    }
}
