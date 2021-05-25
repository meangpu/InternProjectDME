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
        spriteRenderer.sprite = gold.GetSprite();
        value = gold.GetValue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.AddGold(value);
            gameObject.SetActive(false);
        }
    }
}
