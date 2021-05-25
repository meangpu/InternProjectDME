using UnityEngine;

[CreateAssetMenu(fileName = "New Gold Type", menuName = "Collectibles/Create New Gold Type")]
public class Gold : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private int value = 1;

    public Sprite GetSprite() => sprite;
    public int GetValue() => value;
}
