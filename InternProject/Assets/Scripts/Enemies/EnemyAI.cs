using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform turret = null;
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
    private EnemyType enemyType;
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
                TryLookAtPlayer();
                Move();
                FindPlayerInRange();
                break;
        }
    }

    private void OnEnable()
    {
        Setup();
    }

    public void Setup()
    {
        isPassive = enemyDisplay.IsPassive;
        attackRange = enemyDisplay.AttackRange;
        enemyType = enemyDisplay.EnemyType;

        currentTarget = playerBase;

        state = isPassive ? EnemyState.Passive : EnemyState.TargetBase;
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
            state = EnemyState.TargetPlayer;

            switch (enemyType)
            {
                default:
                    currentTarget = player;
                    return;
                case EnemyType.Machine:
                case EnemyType.BossTank:
                case EnemyType.Plane:
                    enemyShoot.StartShooting();
                    return;
            }        
        }
        else
        {
            if (state == EnemyState.TargetBase) { return; }
            SetTargetAsBase();
        }
    }

    private void Move()
    {
        if (!isPassive) // FIX
        {
            float stopRange = enemyType == EnemyType.Machine || enemyType == EnemyType.BossTank ? attackRange : attackRange / 2;

            if (Physics2D.OverlapCircle(transform.position, stopRange, playerLayerMask) != null)
            {
                enemyShoot.StartShooting();
                rb.velocity = Vector2.zero;
                RotateTowardsTarget(currentTarget.position);

                switch (enemyType)
                {
                    default:
                        return;
                    case EnemyType.Machine:
                    case EnemyType.BossTank:
                        LookAtTarget(currentTarget);
                        return; 
                }
            }
            else
            {
                if (state != EnemyState.TargetPlayer)
                {
                    enemyShoot.StopShooting();
                }
            }
        }
   
        rb.velocity = transform.right * enemyDisplay.Speed;

        TryGetNextWaypoint();
    }

    private void TryGetNextWaypoint()
    {
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
        enemyShoot.StopShooting();
        state = EnemyState.TargetBase;
    }

    private void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 lookDirection = targetPosition - transform.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), enemyDisplay.Speed);
    }

    private void LookAtTarget(Transform target)
    {
        if (target == null) { return; }

        Vector3 lookDirection = target.position - turret.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        turret.rotation = Quaternion.RotateTowards(turret.rotation, Quaternion.Euler(0, 0, angle + 90), enemyDisplay.Speed);
    }

    private void TryLookAtPlayer()
    {
        switch (enemyType)
        {
            default:
                return;
            case EnemyType.Machine:
            case EnemyType.BossTank:
                LookAtTarget(player);
                return;
        }
    }
}
