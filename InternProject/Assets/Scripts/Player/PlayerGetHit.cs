using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour, ITargetable
{
    [SerializeField] private PlayerStats playerStats = null;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out BulletEnemy bullet))
        {
            TakeDamage(bullet.Damage);
            DamagePopup.Create(transform.position, bullet.Damage, DamagePopup.DamageType.Player);
            bullet.DestroySelf();
        }
    }

    private void TakeDamage(int dmg)
    {
        playerStats.TakeDamage(dmg);
    }

    public Transform GetTransform() => transform;
}
