using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : MonoBehaviour, IAreaOfDamage
{
    [SerializeField] private LayerMask damageables;

    private int minDamage;
    private int maxDamage;
    private float lifetime;
    private float areaOfDamage;

    public int MinDamage { get => minDamage; set => minDamage = value; }
    public int MaxDamage { get => minDamage; set => minDamage = value; }
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

    private int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    private void DealDamage()
    {
        Collider2D[] damageable = Physics2D.OverlapCircleAll(transform.position, areaOfDamage, damageables);

        foreach (Collider2D collider in damageable)
        {
            if (!collider.TryGetComponent(out IOwnedByPlayer ownedByPlayer)) { continue; }

            ownedByPlayer.TakeDamage(GetRandomDamage());
        }
    }
}
