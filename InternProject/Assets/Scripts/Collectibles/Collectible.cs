using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private ObjCollectible[] collectibles = null;

    private ObjCollectible collectible;
    private CollectibleType collectibleType;
    private float value;

    private Pooler pooler;

    private void Start()
    {
        pooler = PoolingSingleton.Instance.BulletExplosion;
    }

    private void OnEnable()
    {
        RandomizedCollectible();
    }

    private void RandomizedCollectible()
    {
        collectible = collectibles[Random.Range(0, collectibles.Length)];
        collectibleType = collectible.GetCollectibleType();
        spriteRenderer.sprite = collectible.GetSprite();
        spriteRenderer.material = collectible.GetMaterial();
        value = collectible.GetValue();
    }

    private void DestroySelf()
    {
        pooler.ReturnObject(gameObject);
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
