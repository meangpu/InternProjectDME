using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    [SerializeField] private GameObject coin = null;
    [SerializeField] private float magnetSpeed = 5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out TimerSystem timerSystem)) { return; }

        if (!timerSystem.IsActivated(AbilityType.Magnet)) { return; }

        coin.transform.position = Vector3.MoveTowards(coin.transform.position, collision.gameObject.transform.position, magnetSpeed * Time.deltaTime);
    }
}
