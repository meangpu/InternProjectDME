using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : MonoBehaviour, IAreaOfDamage
{
    [SerializeField] private LayerMask damageables;

    private int damage;
    private float lifetime;
    private float areaOfDamage;

    public int Damage { get => damage; set => damage = value; }
    public float Lifetime { get => lifetime; set => lifetime = value; }
    public float AreaOfDamage { get => areaOfDamage; set => areaOfDamage = value; }
    public float BulletSpeed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime > 0) { return; }

        DealDamage();
        DestroySelf();
    }

    private void DestroySelf()
    {
        PoolingSingleton.Instance.EnemyBombPool.ReturnObject(gameObject);
    }

    private void DealDamage()
    {
        Collider2D[] damageable = Physics2D.OverlapCircleAll(transform.position, areaOfDamage, damageables);

        foreach (Collider2D collider in damageable)
        {
            if (!collider.TryGetComponent(out IOwnedByPlayer ownedByPlayer)) { continue; }

            ownedByPlayer.TakeDamage(damage);
        }
    }
}
