using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "PlayerBullet")
        {
            takeDamage(col.gameObject.GetComponent<TankBullet>().damage);
            Destroy(col.gameObject);
        }
    }

    void takeDamage(int damage)
    {
        Debug.Log(damage);
        Destroy(gameObject);
    }

}
