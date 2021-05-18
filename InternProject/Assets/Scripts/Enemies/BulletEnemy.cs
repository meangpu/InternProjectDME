using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float bulletSpeed = 10f;

    public int damage = 10;
    public float lifeTime = 5f;
    [SerializeField] bool isControlled = true;
    private Pooler pool;

    private void Start() {
        // Destroy(gameObject, lifeTime);
        pool = transform.parent.GetComponent<Pooler>();
    }

    private void Update()
    {
        if (isControlled)
        {
            Move();
        }
        
    }

    private void OnEnable() 
    {
        if (isControlled)
        {
            StartCoroutine(DestroyOverTme());
        }
        
    }

    private IEnumerator DestroyOverTme()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

    public void DestroySelf()
    {
        pool.ReturnObject(gameObject);
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }


}
