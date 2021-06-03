using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody2D rb = null;

    private float bulletSpeed;
    private int damage;
    private float lifeTime;
    private bool isActivated = false;

    public int Damage { get => damage; set => damage = value; }
    public float KnockBack { get => 0; set => throw new System.NotImplementedException(); }
    public float Lifetime { get => lifeTime; set => lifeTime = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    private void OnEnable() 
    {
        isActivated = false;
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
        if (isActivated) { return; }

        if (col.gameObject.TryGetComponent(out IOwnedByPlayer ownedByPlayer))
        {
            ownedByPlayer.TakeDamage(damage);
            isActivated = true;
            DestroySelf();
        }
    }
}
