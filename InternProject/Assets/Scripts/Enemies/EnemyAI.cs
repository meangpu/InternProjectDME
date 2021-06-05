using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Seeker seeker = null;
    [SerializeField] private EnemyDisplay enemyDisplay = null;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float pathScanInterval = 0.5f;
    [SerializeField] private float nextWayPointDistance = 1.2f;

    private Transform playerBase = null;
    private Transform player = null;

    private Transform currentTarget = null;
    private Path path;
    private float currentTime = 1f;
    private int currentWaypoint = 0;
    private bool isEnded = false;

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
        TryGetNewPath(Time.deltaTime);

        if (path == null) { return; }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (isEnded) { goto Skip1; } 
            isEnded = true;
            Skip1:
            return;
        }
        else
        {
            if (!isEnded) { goto Skip2; }
            isEnded = false;
        }

        Skip2:
        
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
    }

    private void FindPlayerInRange()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            if (state == EnemyState.TargetPlayer) { return; }
            currentTarget = player;
            state = EnemyState.TargetPlayer;
        }
        else
        {
            if (state == EnemyState.TargetBase) { return; }
            currentTarget = playerBase;
            state = EnemyState.TargetBase;
        }
    }

    private void Move()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.velocity = transform.right * enemyDisplay.Speed;

        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, direction);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, enemyDisplay.Speed);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
    }

    private void TryGetNewPath(float deltaTime)
    {
        currentTime += deltaTime;

        if (currentTime < pathScanInterval) { return; }

        seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
        currentTime = 0f; 
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
