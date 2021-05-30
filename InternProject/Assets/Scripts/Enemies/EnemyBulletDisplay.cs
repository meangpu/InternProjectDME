using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDisplay : MonoBehaviour
{
    [SerializeField] BulletEnemy _bulletScript;
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void setupBullet(ObjEnemyBullet _bullet)
    {
        _spriteRenderer.sprite = _bullet.GetArtWork();
        _spriteRenderer.material = _bullet.GetMaterial();
        _bulletScript.bulletSpeed = _bullet.GetSpeed();
        refreshHitbox();
    }

    private void refreshHitbox()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }



}
