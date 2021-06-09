using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyDisplay enemyDisplay = null;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float nextWayPointDistance = 1.2f;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private EnemyShoot enemyShoot = null;
    [SerializeField] private EnemyPathfinding pathfinding = null;

    private Transform playerBase = null;
    private Transform player = null;

    private Transform currentTarget = null;

    private bool isPassive;
    private float attackRange;
    private EnemyState state;

    private enum EnemyState
    {
        TargetBase,
        TargetPlayer,
        Passive
    }

    private void Awake()
    {
        playerBase = GameObject.FindGameObjectWithTag("Base").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        state = EnemyState.TargetBase;
        currentTarget = playerBase;
    }

    private void Update()
    {
        pathfinding.TryGetNewPath(Time.deltaTime, currentTarget);

        if (pathfinding.Path == null) { return; }

        switch (state)
        {
            default:
                Move();
                break;
            case EnemyState.TargetBase:
                Move();
                FindPlayerInRange();
                break;
            case EnemyState.TargetPlayer:
                Move();
                FindPlayerInRange();
                break;
        }
    }

    private void OnEnable()
    {
        isPassive = enemyDisplay.IsPassive;
        attackRange = enemyDisplay.AttackRange;

        if (isPassive)
        {
            state = EnemyState.Passive;
        }
        else
        {
            state = EnemyState.TargetBase;
        }
    }

    private void FindPlayerInRange()
    {
        if (!player.gameObject.activeInHierarchy)
        {
            if (state == EnemyState.TargetBase) { goto SkipToReturn; }
            SetTargetAsBase();
            SkipToReturn:
            return;
        }

        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            if (state == EnemyState.TargetPlayer) { return; }
            currentTarget = player;
            state = EnemyState.TargetPlayer;
        }
        else
        {
            if (state == EnemyState.TargetBase) { return; }
            SetTargetAsBase();
        }
    }

    private void Move()
    {
        if (!isPassive)
        {
            if (Physics2D.OverlapCircle(transform.position, attackRange / 2, playerLayerMask) != null)
            {
                RotateTowardsTarget(currentTarget.position);
                enemyShoot.StartShooting();
                rb.velocity = Vector2.zero;
                return;
            }
            else
            {
                enemyShoot.StopShooting();
            }
        }

        rb.velocity = transform.right * enemyDisplay.Speed;

        Vector3 currentWaypointPosition = pathfinding.GetCurrentWaypoint();

        RotateTowardsTarget(currentWaypointPosition);

        float distance = Vector2.Distance(rb.position, currentWaypointPosition);

        if (distance < nextWayPointDistance)
        {
            pathfinding.IncrementWaypoint();
        }
    }

    private void SetTargetAsBase()
    {
        currentTarget = playerBase;
        state = EnemyState.TargetBase;
    }

    private void RotateTowardsTarget(Vector3 targetPosition) //currentTarget.position
    {
        Vector3 lookDirection = targetPosition - transform.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), enemyDisplay.Speed);
    }
}
