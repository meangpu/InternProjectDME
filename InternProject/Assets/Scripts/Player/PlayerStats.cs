using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats instance;
    public static PlayerStats Instance { get { return instance; } }

    [SerializeField] private TimerSystem timerSystem = null;
    [SerializeField] private Transform respawnPoint = null;
    [SerializeField] ParticleSystem speedBoostPar;

    // Player Stats
    private int tankLevel = 1;
    private int gunLevel = 1;

    // Gold
    private GoldSystem goldSystem;

    // Health and Energy
    private HealthOrManaSystem healthSystem;
    private HealthOrManaSystem energySystem;
    // ObjPlayerTank Stats from Scriptable Object
    private string tankName;
    private string gunName;
    private int baseMinDamage;
    private int baseMaxDamage;
    private int minDamage;
    private int maxDamage;
    private float healthRegenRate;
    private float timePerHealth;
    private float energyRegenRate;
    private float timePerEnergy;
    private float cooldownBetweenShots;
    private float fireRate;
    private int maxAmmoCount;
    private int currentAmmoCount;
    private float reloadTime;
    private float movementSpeed;
    private float baseMovementSpeed;
    private float rotationSpeed;
    private float bulletLifetime;
    private float bulletSpeed;
    private float bulletKnockback;
    private int tankUpgradeCost;
    private int gunUpgradeCost;
    private ObjPlayerBullet objBullet;

    private float timeElapsedHealth;
    private float timeElapsedEnergy;

    private const int TANK_MAX_LEVEL_LIMIT = 3;

    private ObjTankTurret turret;
    private ObjPlayerTank tank;

    // Events
    public event Action<int, int> OnAmmoUpdated;
    public event Action<int, int> OnTankLeveledUp;
    public event Action<int, int> OnGunLeveledUp;
    public event Action OnEnergyShieldDisabled;
    public event Action OnPlayerRespawned;
    public event Action OnMovementStatsChanged;

    public UnityEvent OnEnergyShieldActivated;
    public UnityEvent OnGoldCollected;

    // Vars for abilities that tweaked stuff
    private PlayerAbilities playerAbilities;
    private bool energyShieldEnabled = false;
    private bool isImmuned = false;

    private UIManager uiManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerAbilities = GetComponent<PlayerAbilities>();
    }

    private void Start()
    {
        Player player = GetComponent<Player>();
        turret = player.GetTurret();
        tank = player.GetTank();

        uiManager = UIManager.Instance;

        int level = tankLevel - 1;

        healthSystem = new HealthOrManaSystem(tank.GetHealth()[level]);
        energySystem = new HealthOrManaSystem(tank.GetEnergy()[level]);

        goldSystem = new GoldSystem(GameManager.Instance.GetStartingGold());

        tankName = tank.GetName();
        gunName = turret.GetName();

        UpdateGunStats();
        UpdateTankStats();

        currentAmmoCount = maxAmmoCount;
        bulletLifetime = turret.GetLifetime();
        bulletKnockback = turret.GetKnockBack();

        objBullet = turret.GetBulletType();

        playerAbilities.OnTriggerEnergyShield += HandleToggleEnergyShield;
        timerSystem.OnTimerFinished += HandleTimerFinished;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        RegenerateHealth(deltaTime);

        if (energyShieldEnabled) { return; } // Using Energy shield does not regenerate energy

        RegenerateEnergy(deltaTime);

    }

    private void RegenerateHealth(float deltaTime)
    {
        if (healthSystem.GetAmount() == 0) { return; } // IF alive

        timeElapsedHealth += deltaTime;

        if (timeElapsedHealth < timePerHealth) { return; }

        healthSystem.Heal(1);
        timeElapsedHealth -= timePerHealth;
    }

    private void RegenerateEnergy(float deltaTime)
    {
        timeElapsedEnergy += deltaTime;

        if (timeElapsedEnergy < timePerEnergy) { return; }

        energySystem.Heal(1);
        timeElapsedEnergy -= timePerEnergy;
    }

    private void UpdateAmmoUI()
    {
        OnAmmoUpdated?.Invoke(currentAmmoCount, maxAmmoCount);
    }

    private void UpdateTankStats()
    {
        int level = tankLevel - 1;

        healthSystem.SetNewMax(tank.GetHealth()[level]);
        energySystem.SetNewMax(tank.GetEnergy()[level]);

        healthRegenRate = tank.GetHealthRegenRate()[level];
        timePerHealth = 1 / healthRegenRate;

        energyRegenRate = tank.GetEnergyRegenRate()[level];
        timePerEnergy = 1 / energyRegenRate;

        baseMovementSpeed = tank.GetMovementSpeed()[level];
        movementSpeed = baseMovementSpeed;
        rotationSpeed = tank.GetRotationSpeed()[level];

        if (tankLevel == TANK_MAX_LEVEL_LIMIT) { return; }

        tankUpgradeCost = tank.GetUpgradeCost()[level];
    }

    private void UpdateGunStats()
    {
        int level = gunLevel - 1;

        fireRate = turret.GetRateOfFire()[level];
        cooldownBetweenShots = 1 / fireRate;

        maxAmmoCount = turret.GetAmmoCount()[level];

        reloadTime = turret.GetReloadTime()[level];

        baseMinDamage = turret.GetMinDamage()[level];
        baseMaxDamage = turret.GetMaxDamage()[level];
        minDamage = baseMinDamage;
        maxDamage = baseMaxDamage;

        bulletSpeed = turret.GetBulletSpeed()[level];

        if (gunLevel == TANK_MAX_LEVEL_LIMIT) { return; }

        gunUpgradeCost = turret.GetUpgradeCost()[level];
    }

    public void RespawnPlayer()
    {
        Transform playerTank = gameObject.transform;
        playerTank.SetPositionAndRotation(respawnPoint.position, respawnPoint.rotation);

        healthSystem.Heal(100, HealthOrManaSystem.HealingType.Percentage);
        energySystem.SetAmount(20);

        currentAmmoCount = maxAmmoCount;
        UpdateAmmoUI();

        OnPlayerRespawned?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (isImmuned) 
        {   
            DamagePopup.Create(transform.position, 0, DamagePopup.DamageType.Player);
            return; 
        }

        DamagePopup.Create(transform.position, damage, DamagePopup.DamageType.Player);
        if (!energyShieldEnabled)
        {
            healthSystem.Damage(damage);
        } 
        else
        {
            int leftoverDamage = damage;
            int energyRemaining = energySystem.GetAmount();

            if (energyRemaining >= damage)
            {
                energySystem.Damage(damage);

                if (energyRemaining != 0) { return; }

                HandleToggleEnergyShield();
            }
            else
            {
                leftoverDamage -= energyRemaining;
                energySystem.Damage(energyRemaining);
                healthSystem.Damage(leftoverDamage);

                HandleToggleEnergyShield();
            }
        }

        if (healthSystem.GetAmount() > 0) { return; }

        timerSystem.ClearAllTimer();
        GameManager.Instance.HandlePlayerDeath();
    }

    public bool TrySpendEnergy(int energy)
    {
        if (energySystem.GetAmount() < energy)
        {
            uiManager.TriggerNotEnoughEnergy();
            return false;
        }
        else
        {
            energySystem.Damage(energy);
            return true;
        }
    }

    public void AddGold(int goldToAdd)
    {
        goldSystem.AddGold(goldToAdd);
        OnGoldCollected?.Invoke();
    }

    public bool SpendGold(int goldUsed)
    {
        return goldSystem.TrySpendGold(goldUsed);
    }

    public int DealDamage()
    {
        return UnityEngine.Random.Range(minDamage, maxDamage + 1);
    }

    private void HandleTimerFinished(AbilityType abilityType)
    {
        switch (abilityType)
        {
            default:
                return;

            case AbilityType.IncendiaryAmmo:
                RemoveDamageBoost();
                return;

            case AbilityType.SpeedBoost:
                RemoveSpeedBoost();
                return;
        }
    }

    public void AddSpeedBoost(float amount, float duration)
    {
        movementSpeed = baseMovementSpeed + amount;
        speedBoostPar.Play();
        timerSystem.PutOnTimer(AbilityType.SpeedBoost, duration);
        OnMovementStatsChanged?.Invoke();
    }

    private void RemoveSpeedBoost()
    {
        movementSpeed = baseMovementSpeed;
        speedBoostPar.Stop();
        OnMovementStatsChanged?.Invoke();
    }

    public void AddDamageBoost(float percentage)
    {
        minDamage = Mathf.CeilToInt(minDamage * percentage);
        maxDamage = Mathf.CeilToInt(maxDamage * percentage);
    }

    private void RemoveDamageBoost()
    {
        minDamage = baseMinDamage;
        maxDamage = baseMaxDamage;
    }

    public void TankLevelUp()
    {
        if (tankLevel == TANK_MAX_LEVEL_LIMIT) { return; }

        if (!SpendGold(tankUpgradeCost)) { return; }

        float addedSpeed = movementSpeed - baseMovementSpeed;

        tankLevel++;
        UpdateTankStats();

        if (gameObject.activeInHierarchy)
        {
            healthSystem.Heal(10, HealthOrManaSystem.HealingType.Percentage);
            energySystem.Heal(10, HealthOrManaSystem.HealingType.Percentage);
        }

        movementSpeed = baseMovementSpeed + addedSpeed;

        OnMovementStatsChanged?.Invoke();
        OnTankLeveledUp?.Invoke(tankLevel, TANK_MAX_LEVEL_LIMIT);
    }

    public void GunLevelUp()
    {
        if (gunLevel == TANK_MAX_LEVEL_LIMIT) { return; }

        if (!SpendGold(gunUpgradeCost)) { return; }

        int addedMinDamage = minDamage - baseMinDamage;
        int addedMaxDamage = maxDamage - baseMaxDamage;

        gunLevel++;
        UpdateGunStats();
        UpdateAmmoUI();

        minDamage = baseMinDamage + addedMinDamage;
        maxDamage = baseMaxDamage + addedMaxDamage;

        OnGunLeveledUp?.Invoke(gunLevel, TANK_MAX_LEVEL_LIMIT); 
    }
    
    public void GetNextLevelTankStats(out List<(bool, string)> statsList)
    {
        int currentLevel = tankLevel - 1;

        int[] health = tank.GetHealth();
        int healthNow = health[currentLevel];

        float[] healthRegen = tank.GetHealthRegenRate();

        int[] energy = tank.GetEnergy();
        int energyNow = energy[currentLevel];

        float[] energyRegen = tank.GetEnergyRegenRate();

        float[] speed = tank.GetMovementSpeed();

        float[] rotSpeed = tank.GetRotationSpeed();

        if (tankLevel == TANK_MAX_LEVEL_LIMIT)
        {
            statsList = new List<(bool, string)>
            {
                (false, healthNow.ToString()),
                (false, healthRegenRate.ToString()),
                (false, energyNow.ToString()),
                (false, energyRegenRate.ToString()),
                (false, movementSpeed.ToString()),
                (false, rotationSpeed.ToString())
            };
        }
        else
        {
            statsList = new List<(bool, string)>
            {
                healthNow == health[tankLevel] ? (false, healthNow.ToString()) : (true, $"{healthNow} > {health[tankLevel]}"),
                healthRegenRate == healthRegen[tankLevel] ? (false, healthRegenRate.ToString()) : (true, $"{healthRegenRate} > {healthRegen[tankLevel]}"),
                energyNow == energy[tankLevel] ? (false, energyNow.ToString()) : (true, $"{energyNow} > {energy[tankLevel]}"),
                energyRegenRate == energyRegen[tankLevel] ? (false, energyRegenRate.ToString()) : (true, $"{energyRegenRate} > {energyRegen[tankLevel]}"),
                movementSpeed == speed[tankLevel] ? (false, movementSpeed.ToString()) : (true, $"{movementSpeed} > {speed[tankLevel]}"),
                rotationSpeed == rotSpeed[tankLevel] ? (false, rotationSpeed.ToString()) : (true, $"{rotationSpeed} > {rotSpeed[tankLevel]}")
            };
        }
    }

    public void GetNextLevelGunStats(out List<(bool, string)> statsList)
    {
        int[] minDamageList = turret.GetMinDamage();

        int[] maxDamageList = turret.GetMaxDamage();

        int[] ammoCountList = turret.GetAmmoCount();

        float[] rateOfFireList = turret.GetRateOfFire();

        float[] reloadSpeedList = turret.GetReloadTime();

        float[] bulletSpeedList = turret.GetBulletSpeed();

        if (gunLevel == TANK_MAX_LEVEL_LIMIT)
        {
            statsList = new List<(bool, string)>
            {
                (false, $"{minDamage}-{maxDamage}"),
                (false, maxAmmoCount.ToString()),
                (false, fireRate.ToString()),
                (false, reloadTime.ToString()),
                (false, bulletSpeed.ToString())
            };
        }
        else
        {
            statsList = new List<(bool, string)>
            {
                maxDamage == maxDamageList[gunLevel] ? (false, $"{minDamage}-{maxDamage}") : (true, $"{minDamage}-{maxDamage} > {minDamageList[gunLevel]}-{maxDamageList[gunLevel]}"),
                maxAmmoCount == ammoCountList[gunLevel] ? (false, maxAmmoCount.ToString()) : (true, $"{maxAmmoCount} > {ammoCountList[gunLevel]}"),
                fireRate == rateOfFireList[gunLevel] ? (false, fireRate.ToString()) : (true, $"{fireRate} > {rateOfFireList[gunLevel]}"),
                reloadTime == reloadSpeedList[gunLevel] ? (false, reloadTime.ToString()) : (true, $"{reloadTime} > {reloadSpeedList[gunLevel]}"),
                bulletSpeed == bulletSpeedList[gunLevel] ? (false, bulletSpeed.ToString()) : (true, $"{bulletSpeed} > {bulletSpeedList[gunLevel]}")
            };
        }
    }

    public void SetIsImmuned(bool value)
    {
        isImmuned = value;
    }

    private void HandleToggleEnergyShield()
    {
        OnEnergyShieldActivated?.Invoke();
        energyShieldEnabled = !energyShieldEnabled;

        if (energyShieldEnabled) { return; }
        OnEnergyShieldDisabled?.Invoke();
    }

    private void OnDestroy()
    {
        playerAbilities.OnTriggerEnergyShield -= HandleToggleEnergyShield;
        timerSystem.OnTimerFinished -= HandleTimerFinished;
    }

    #region Stats Retrieving

    public Sprite GetTankSprite() => tank.GetSprite();
    public Sprite GetGunSprite() => turret.GetSprite();

    public HealthOrManaSystem GetHealthSystem() => healthSystem;
    public HealthOrManaSystem GetEnergySystem() => energySystem;

    public GoldSystem GetGoldSystem() => goldSystem;

    public string GetTankName() => tankName;
    public string GetGunName() => gunName;

    public int GetTankLevel() => tankLevel;
    public int GetGunLevel() => gunLevel;
    public int GetMaxLevel() => TANK_MAX_LEVEL_LIMIT;

    public int GetMaxAmmoCount() => maxAmmoCount;
    public int GetCurrentAmmoCount() => currentAmmoCount;

    public void DecreaseCurrentAmmoCount(int decrement = 1)
    {
        currentAmmoCount -= decrement;
        UpdateAmmoUI();
    }
    public void EmptyCurrentAmmoCount()
    {
        currentAmmoCount = 0;
        UpdateAmmoUI();
    }
    public void ReloadAmmoCount()
    {
        currentAmmoCount = maxAmmoCount;
        UpdateAmmoUI();
    }

    public float GetCoolDownBetweenShots() => cooldownBetweenShots;

    public float GetReloadTime() => reloadTime;

    public float GetMovementSpeed() => movementSpeed;
    public float GetRotationSpeed() => rotationSpeed;

    public float GetBulletLifetime() => bulletLifetime;
    public float GetBulletSpeed() => bulletSpeed;
    public float GetKnockbackValue() => bulletKnockback;

    public int GetTankUpgradeCost() => tankUpgradeCost;
    public int GetGunUpgradeCost() => gunUpgradeCost;

    public ObjPlayerBullet GetBulletType() => objBullet;

    #endregion
}
