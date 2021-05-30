using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDisplay : MonoBehaviour
{
    [SerializeField] TankBullet _bulletScript;
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void setupBullet(ObjPlayerBullet _bullet)
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
