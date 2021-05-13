using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    private Pooler pool;

    private void Start() 
    {
        pool = transform.parent.GetComponent<Pooler>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("PlayerBullet"))
        {
            TankBullet bullet = col.gameObject.GetComponent<TankBullet>();
            TakeDamage(bullet.damage);
            bullet.DestroySelf();
        }
    }

    private void TakeDamage(int damage)
    {
        // add check if hp <= 0
        pool.ReturnObject(gameObject);
    }

}
