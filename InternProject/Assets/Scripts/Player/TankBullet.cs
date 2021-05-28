using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private DamageSetter damageSetter = null;
    [SerializeField] BoxCollider2D boxCollider = null;

    // Temporary variables
    [SerializeField] private float bulletSpeed = 10f;
    public float lifeTime = 4f;

    private int damage;
    public float knockBack;

    public int Damage { get { return damage; } }
    public Rigidbody2D GetRB() => rb;
    public BoxCollider2D GetBox() => boxCollider;


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
}
