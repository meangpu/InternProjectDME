using UnityEngine;

public interface IEnemy
{
    public void TakeDamage(int damage);
    public void TakeKnockback(Vector2 attackerPos, float knockbackForce);
}