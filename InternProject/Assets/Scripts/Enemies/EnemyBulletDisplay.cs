using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void SetupBullet(ObjEnemyBullet _bullet)
    {
        _spriteRenderer.sprite = _bullet.GetArtWork();
        _spriteRenderer.material = _bullet.GetMaterial();
    }
}
