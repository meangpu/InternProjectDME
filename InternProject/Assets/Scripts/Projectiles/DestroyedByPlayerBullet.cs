using UnityEngine;

public class DestroyedByPlayerBullet : MonoBehaviour
{
    [SerializeField] private BulletEnemy enemyBullet = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TankBullet bullet))
        {
            bullet.DestroySelf();
            enemyBullet.DestroySelf();
        }
    }
}
