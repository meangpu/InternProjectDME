using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float bulletSpeed = 10f;

    public int damage = 10;
    public float lifeTime = 5f;
    private pooler pool;

    private void Start() {
        // Destroy(gameObject, lifeTime);
        pool = transform.parent.GetComponent<pooler>();
    }

    private void Update()
    {
        Move();
    }

    private void OnEnable() 
    {
        StartCoroutine(DestroyOverTme());
    }

    IEnumerator DestroyOverTme()
    {
        yield return new WaitForSeconds(lifeTime);
        pool.ReturnObject(gameObject);
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }


}
