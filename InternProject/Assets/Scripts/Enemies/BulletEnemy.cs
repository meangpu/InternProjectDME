using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private DamageSetter damageSetter = null;
    public float bulletSpeed = 10f;

    private int damage;

    public int Damage { get { return damage; } }

    public float lifeTime = 5f;

    private void OnEnable() 
    {
        damage = damageSetter.Damage;
        Move();
        StartCoroutine(DestroyOverTme());
    }

    private IEnumerator DestroyOverTme()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

    public void DestroySelf()
    {
        PoolingSingleton.Instance.EnemyBulletPool.ReturnObject(gameObject);
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IOwnedByPlayer ownedByPlayer))
        {
            ownedByPlayer.TakeDamage(damage);
            DestroySelf();
        }
    }
}
