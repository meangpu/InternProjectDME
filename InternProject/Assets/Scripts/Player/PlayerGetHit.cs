using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("EnemyBullet"))
        {
            BulletEnemy bullet = col.gameObject.GetComponent<BulletEnemy>();
            TakeDamage(bullet.damage);
            bullet.DestroySelf();
        }
    }

    void TakeDamage(int dmg)
    {
        // Debug.Log(dmg);
    }
}
