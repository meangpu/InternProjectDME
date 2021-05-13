using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    private pooler pool;

    private void Start() 
    {
        pool = transform.parent.GetComponent<pooler>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(col.gameObject.GetComponent<TankBullet>().damage);
            Destroy(col.gameObject);  // retuen player bulett yo pool
        }
    }

    void TakeDamage(int damage)
    {
        // Debug.Log(damage);
        // Destroy(gameObject);

        // add check if hp <= 0
        pool.ReturnObject(gameObject);

    }

}
