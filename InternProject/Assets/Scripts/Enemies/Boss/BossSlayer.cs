using UnityEngine;
using UnityEngine.Events;

public class BossSlayer : MonoBehaviour
{
    [SerializeField] private Enemy enemy = null;
    [SerializeField] private EnemyAI enemyAI = null;
    [SerializeField] private EnemyDisplay enemyDisplay = null;
    [SerializeField] private float shieldActivationTime = 3f;
    [SerializeField] private float shieldCooldown = 15f;
    [SerializeField] private float missileCooldown = 10f;
    [SerializeField] private Transform[] missileSpawnPoints = null;
    [SerializeField] private Vector2 missileDamage;

    private float shieldTime = 0f;
    private float shieldCooldownTime = 0f;
    private float missileCooldownTime = 10f;

    private bool isShieldActivated = false;

    private PoolingSingleton pooler;

    public UnityEvent OnEnergyShieldActivated;
    public UnityEvent OnEnergyShieldDeactivated;

    private void Start()
    {
        pooler = PoolingSingleton.Instance;
        enemyAI.Setup();
    }

    private void Update()
    {
        ProcessCooldowns(Time.deltaTime);
        TryDisableShield();

        ShootMissiles();
    }

    private void ProcessCooldowns(float deltaTime)
    {
        shieldTime = Mathf.Max(shieldTime - deltaTime, 0f);
        shieldCooldownTime = Mathf.Max(shieldCooldownTime - deltaTime, 0f);
        missileCooldownTime = Mathf.Max(missileCooldownTime - deltaTime, 0f);
    }

    private void TryDisableShield()
    {
        if (shieldTime > 0f) { return; }

        if (!isShieldActivated) { return; }

        PutShieldOnCooldown();
        enemy.SetImmortalState(false);

        OnEnergyShieldDeactivated?.Invoke();
    }

    private void PutShieldOnCooldown()
    {
        isShieldActivated = false;
        shieldCooldownTime = shieldCooldown;
    }

    public void ActivateShield()
    {
        if (isShieldActivated) { return; }

        if (shieldCooldownTime > 0f) { return; }

        isShieldActivated = true;
        shieldTime = shieldActivationTime;
        enemy.SetImmortalState(true);

        OnEnergyShieldActivated?.Invoke();
    }

    private void ShootMissiles()
    {
        if (missileCooldownTime != 0) { return; }

        foreach (Transform spawnpoint in missileSpawnPoints)
        {
            pooler.EnemyMissilePool.SpawnEnemyMissile(spawnpoint.position, spawnpoint.rotation, RandomMissileDamage(), Random.Range(4f, 8f), enemyDisplay.BulletLifetime);
        }

        missileCooldownTime = missileCooldown;
    }

    private int RandomMissileDamage()
    {
        return Random.Range((int)missileDamage.x, (int)missileDamage.y + 1);
    }
}
