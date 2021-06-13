using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private bool isMissile = false;

    private float bulletSpeed;
    private int damage;
    private float lifeTime;
    private bool isActivated = false;

    private PoolingSingleton pooler;

    public int Damage { get => damage; set => damage = value; }
    public float KnockBack { get => 0; set => throw new System.NotImplementedException(); }
    public float Lifetime { get => lifeTime; set => lifeTime = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

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

    private void Update()
    {
        if (!isMissile) { return; }

        Move();
    }

    private IEnumerator DestroyOverTme()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

    public void DestroySelf()
    {
        pooler.BulletExplosion.SpawnBasicObject(transform.position, transform.rotation);

        if (isMissile)
        {
            pooler.EnemyMissilePool.ReturnObject(gameObject);
        }
        else
        {
            pooler.EnemyBulletPool.ReturnObject(gameObject);
        }
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
