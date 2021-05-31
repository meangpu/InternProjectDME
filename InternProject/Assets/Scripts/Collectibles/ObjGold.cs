using UnityEngine;

[CreateAssetMenu(fileName = "New ObjGold Type", menuName = "Collectibles/Create New ObjGold Type")]
public class ObjGold : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private int value = 1;
    [SerializeField] Material material;

    public Sprite GetSprite() => sprite;
    public int GetValue() => value;
    public Material GetMaterial() => material;
}
