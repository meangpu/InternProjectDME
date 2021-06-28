using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    [SerializeField] private float magnetSpeed = 5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out TimerSystem timerSystem)) { return; }

        if (!timerSystem.IsActivated(AbilityType.Magnet)) { return; }

        transform.position = Vector3.MoveTowards(transform.position, collision.gameObject.transform.position, magnetSpeed * Time.deltaTime);
    }
}
