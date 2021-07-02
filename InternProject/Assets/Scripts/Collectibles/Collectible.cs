using UnityEngine;

public class Collectible : MonoBehaviour
{
    enum PowerType {hp, energy, speed};

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private ObjCollectible[] collectibles = null;
    [SerializeField] bool LockSpawn;
    [SerializeField] PowerType lockType;
    [SerializeField] private ObjSound sound = null;

    private ObjCollectible collectible;
    private CollectibleType collectibleType;
    private float value;

    private Pooler pooler;

    private void Start()
    {
        pooler = PoolingSingleton.Instance.CollectiblePool;
    }

    private void OnEnable()
    {
        if (!LockSpawn)
        {
            RandomizedCollectible();
        }
        else
        {
            SpawnByCheckLockType();
        }
    }

    void SpawnByCheckLockType()
    {
        switch (lockType)
        {
        case PowerType.hp:
            SpawnValueType(collectibles[0]);
            break;
        case PowerType.energy:
            SpawnValueType(collectibles[1]);
            break;
        case PowerType.speed:
            SpawnValueType(collectibles[2]);
            break;
        }
    }

    void SpawnValueType(ObjCollectible _objType)
    {
        collectibleType = _objType.GetCollectibleType();
        spriteRenderer.sprite = _objType.GetSprite();
        spriteRenderer.material = _objType.GetMaterial();
        value = _objType.GetValue();
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

        PoolingSingleton.Instance.AudioSourcePool.SpawnAudioSource(transform.position, transform.rotation, sound);
        DestroySelf();
    }
}
