using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    // Temporary variables
    [SerializeField] private float bulletSpeed = 10f;
    public int damage = 10;
    public float lifeTime = 4f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }
}
