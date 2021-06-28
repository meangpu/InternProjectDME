using UnityEngine;

public class CoinHitbox : MonoBehaviour
{
    [SerializeField] private AssignGold gold = null;
    [SerializeField] private GameObject coin = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.AddGold(gold.Value);
            PoolingSingleton.Instance.GoldPool.ReturnObject(coin);
        }
    }
}
