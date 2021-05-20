using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats = null;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("EnemyBullet"))
        {
            BulletEnemy bullet = col.gameObject.GetComponent<BulletEnemy>();
            TakeDamage(bullet.damage);
            bullet.DestroySelf();
        }
    }

    private void TakeDamage(int dmg)
    {
        playerStats.TakeDamage(dmg);
    }
}
