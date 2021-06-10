using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    
    private float rotationSpeed = 0f;

    private Transform target;
    private TargetType targetType;

    private float timeScanElapsed = 0;
    private const float TARGET_SCAN_INTERVAL = 0.1f;

    public enum TargetType
    {
        Player,
        Enemy
    }

    private void Update()
    {
        timeScanElapsed += Time.deltaTime;

        if (timeScanElapsed >= TARGET_SCAN_INTERVAL)
        {
            timeScanElapsed = 0;
            FindTarget(targetType);
        }

        if (target != null)
        {
            AimAtTarget();
        }
    }

    public void Setup(TargetType targetType, float rotationSpeed = 200f)
    {
        this.targetType = targetType;
        this.rotationSpeed = rotationSpeed;
    }

    private void AimAtTarget()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotationSpeed; 
    }

    private void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void FindTarget(TargetType targetType)
    {
        if (target != null && target.gameObject.activeSelf)
        {
            return;
        }

        ITargetable targetable = null;

        switch (targetType)
        {
            case TargetType.Player:
                GameManager gameManager = GameManager.Instance;
                Player player = gameManager.GetPlayer();

                if (player.gameObject.activeInHierarchy) // Player is alive <-- CHANGE LATER
                {
                    targetable = player.GetComponent<ITargetable>();
                }
                else // player is dead, target the base
                {
                    targetable = gameManager.GetPlayerBase().GetComponent<ITargetable>();
                }
                break;

            case TargetType.Enemy:
                // Choose a random enemy to lock on.
                List<Enemy> enemyList = WaveManager.Instance.EnemyList;
                int randomTargetIndex = Random.Range(0, enemyList.Count);
                Enemy enemyTarget = enemyList[randomTargetIndex];

                targetable = enemyTarget.GetComponent<ITargetable>();
                break;
        }

        SetTarget(targetable.GetTransform());
    }
}
