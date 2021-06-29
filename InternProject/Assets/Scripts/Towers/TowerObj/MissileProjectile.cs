using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour, IAreaOfDamage
{
    [SerializeField] private Rigidbody2D rb = null;

    private float bulletSpeed;
    private float lifetime;
    private int minDamage;
    private int maxDamage;
    private float range;
    private bool isActivated = false;

    private PoolingSingleton pooler;
    public int MinDamage { get => minDamage; set => minDamage = value; }
    public int MaxDamage { get => maxDamage; set => maxDamage = value; }
    public float Lifetime { get => lifetime; set => lifetime = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float AreaOfDamage { get => range; set => range = value; }


    private void Start()
    {
        pooler = PoolingSingleton.Instance;
    }

    private void OnEnable()
    {
        isActivated = false;
        Move();
        StartCoroutine(DestroyOverTme());
    }

    public void DestroySelf()
    {
        pooler.BulletExplosion.SpawnBasicObject(transform.position, transform.rotation);
        pooler.TowerMissilePool.ReturnObject(gameObject);
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

    private int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
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
                    enemy.TakeDamage(GetRandomDamage());
                }
            }
            
            isActivated = true;
            DestroySelf();
        }
    }
}
