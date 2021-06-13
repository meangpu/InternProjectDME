using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private ObjCollectible[] collectibles = null;

    private ObjCollectible collectible;
    private CollectibleType collectibleType;
    private float value;

    private void OnEnable()
    {
        RandomizedCollectible();
    }

    private void RandomizedCollectible()
    {
        collectible = collectibles[Random.Range(0, collectibles.Length)];
        collectibleType = collectible.GetCollectibleType();
        spriteRenderer.sprite = collectible.GetSprite();
        value = collectible.GetValue();
    }

    private void DestroySelf()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out PlayerStats playerStats)) { return; }

        switch (collectibleType)
        {
            case CollectibleType.Health:
                playerStats.GetHealthSystem().Heal((int)value);
                break;
            case CollectibleType.Energy:
                playerStats.GetEnergySystem().Heal((int)value);
                break;
            case CollectibleType.Speed:
                playerStats.AddSpeedBoost(value, 15f);
                break;
        }

        DestroySelf();
    }
}
