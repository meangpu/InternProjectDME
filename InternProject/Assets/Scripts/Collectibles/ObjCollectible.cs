using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible", menuName = "Collectibles/Create New Collectible")]
public class ObjCollectible : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private CollectibleType collectibleType;
    [SerializeField] private float value = 0f;

    public Sprite GetSprite() => sprite;
    public CollectibleType GetCollectibleType() => collectibleType;
    public float GetValue() => value;
}
