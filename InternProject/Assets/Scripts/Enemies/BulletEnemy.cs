using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private DamageSetter damageSetter = null;

    private int damage;

    public int Damage { get { return damage; } }

    public float lifeTime = 5f;

    private void Update()
    {
        Move(); 
    }

    private void OnEnable() 
    {
        damage = damageSetter.Damage;
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

}
