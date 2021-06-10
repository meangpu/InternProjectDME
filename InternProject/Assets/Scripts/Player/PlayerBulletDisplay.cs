using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void SetupBullet(ObjPlayerBullet _bullet)
    {
        _spriteRenderer.sprite = _bullet.GetArtWork();
        _spriteRenderer.material = _bullet.GetMaterial();
    }
}
