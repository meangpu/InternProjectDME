using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 200f;

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

        Move();
    }

    public void Setup(TargetType targetType, float movementSpeed = 2f, float rotationSpeed = 200f)
    {
        this.targetType = targetType;
        this.movementSpeed = movementSpeed;
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
                PlayerGetHit player = GameManager.Instance.GetPlayer();
                if (player.gameObject.activeSelf) // Player is alive <-- CHANGE LATER
                {
                    targetable = player.GetComponent<ITargetable>();
                }
                else // player is dead, target the base
                {
                    targetable = 
                }
                
                
                break;
            case TargetType.Enemy:
                List<EnemyGetHit> enemyList = WaveManager.Instance.EnemyList;

                int randomTargetIndex = Random.Range(0, enemyList.Count);
                EnemyGetHit enemyTarget = enemyList[randomTargetIndex];
                break;
        }

    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * movementSpeed;
    }
}
