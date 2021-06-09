using UnityEngine;

[CreateAssetMenu(fileName = "New ObjGold Type", menuName = "Collectibles/Create New ObjGold Type")]
public class ObjGold : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private int value = 1;
    [SerializeField] Material material;
    [SerializeField] Material parMaterial;

    public Sprite GetSprite() => sprite;
    public int GetValue() => value;
    public Material GetMaterial() => material;
    public Material GetParMaterial() => parMaterial;
}
