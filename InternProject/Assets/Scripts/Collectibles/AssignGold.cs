using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignGold : MonoBehaviour
{
    [SerializeField] private ObjGold ObjGold = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] ParticleSystemRenderer parEffect;

    private int value;

    private void Awake()
    {
        if (ObjGold != null)
        {
            SetGold(ObjGold);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.AddGold(value);
            PoolingSingleton.Instance.GoldPool.ReturnObject(gameObject);
        }
    }

    public void SetGold(ObjGold _newGold)
    {
        ObjGold = _newGold;
        spriteRenderer.sprite = ObjGold.GetSprite();
        spriteRenderer.material = ObjGold.GetMaterial();
        parEffect.material = ObjGold.GetParMaterial();
        value = ObjGold.GetValue();
    }
}
