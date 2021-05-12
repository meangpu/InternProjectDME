using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(col.gameObject.GetComponent<TankBullet>().damage);
            Destroy(col.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        Debug.Log(damage);
        Destroy(gameObject);
    }

}
