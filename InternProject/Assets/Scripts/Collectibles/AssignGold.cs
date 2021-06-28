using UnityEngine;

public class AssignGold : MonoBehaviour
{
    [SerializeField] private ObjGold ObjGold = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] ParticleSystemRenderer parEffect;

    private int coinValue;

    public int Value { get => coinValue; set => coinValue = value; }

    private void Awake()
    {
        if (ObjGold != null)
        {
            SetGold(ObjGold);
        }
    }

    public void SetGold(ObjGold _newGold)
    {
        ObjGold = _newGold;
        spriteRenderer.sprite = ObjGold.GetSprite();
        spriteRenderer.material = ObjGold.GetMaterial();
        parEffect.material = ObjGold.GetParMaterial();
        coinValue = ObjGold.GetValue();
    }
}
