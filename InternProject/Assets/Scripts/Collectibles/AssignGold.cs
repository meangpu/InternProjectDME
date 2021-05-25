using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignGold : MonoBehaviour
{
    [SerializeField] private Gold gold = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private int value;

    private void Awake()
    {
        if (gold != null)
        {
            spriteRenderer.sprite = gold.GetSprite();
            value = gold.GetValue();
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

    public void setGold(Gold _newGold)
    {
        gold = _newGold;
        spriteRenderer.sprite = gold.GetSprite();
        value = gold.GetValue();
    }
}
