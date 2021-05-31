using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignGold : MonoBehaviour
{
    [SerializeField] private ObjGold ObjGold = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private int value;

    private void Awake()
    {
        if (ObjGold != null)
        {
            spriteRenderer.sprite = ObjGold.GetSprite();
            value = ObjGold.GetValue();
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

    public void setGold(ObjGold _newGold)
    {
        ObjGold = _newGold;
        spriteRenderer.sprite = ObjGold.GetSprite();
        spriteRenderer.material = ObjGold.GetMaterial();
        value = ObjGold.GetValue();
    }
}
