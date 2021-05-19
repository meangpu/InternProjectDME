using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    [Header("Temp Fields")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] public int damage = 10;

    private void Update()
    {
        rb.velocity = (Vector2)transform.up * bulletSpeed;
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyOverTme());
    }

    public void DestroySelf()
    {
        PoolingSingleton.Instance.TowerBulletPool.ReturnObject(gameObject);
    }

    private IEnumerator DestroyOverTme()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

}
