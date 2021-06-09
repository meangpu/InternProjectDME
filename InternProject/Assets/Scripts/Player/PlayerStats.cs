using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats instance;
    public static PlayerStats Instance { get { return instance; } }

    // Player Stats
    private readonly int startingGold = 2000;
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
    private float rotationSpeed;
    private float bulletLifetime;
    private float bulletSpeed;
    private float bulletKnockback;
    private ObjPlayerBullet objBullet;

    private float timeElapsedHealth;
    private float timeElapsedEnergy;

    private const int TANK_MAX_LEVEL_LIMIT = 3;

    private ObjTankTurret turret;
    private ObjPlayerTank tank;

    // Events
    public event Action<int, int> OnAmmoUpdated;
    public event Action<int> OnTankLeveledUp;
    public event Action<int> OnGunLeveledUp;
    public event Action OnEnergyShieldDisabled;
    
    // Vars for abilities that tweaked stuff
    private PlayerAbilities playerAbilities;
    private bool energyShieldEnabled = false;
    private bool isImmuned = false;
    private bool incendiaryAmmoEnabled = false;
    private float damageBoostDuration = 0;

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

        healthSystem = new HealthOrManaSystem(tank.GetHealth()[tankLevel - 1]);
        energySystem = new HealthOrManaSystem(tank.GetEnergy()[tankLevel - 1]);

        goldSystem = new GoldSystem(startingGold);

        tankName = tank.GetName();
        gunName = turret.GetName();

        healthRegenRate = tank.GetHealthRegenRate()[tankLevel - 1];
        timePerHealth = 1 / healthRegenRate;

        energyRegenRate = tank.GetEnergyRegenRate()[tankLevel - 1];
        timePerEnergy = 1 / energyRegenRate;

        fireRate = turret.GetRateOfFire()[gunLevel - 1];
        cooldownBetweenShots = 1 / fireRate;

        maxAmmoCount = turret.GetAmmoCount()[gunLevel - 1];
        currentAmmoCount = maxAmmoCount;

        reloadTime = turret.GetReloadTime()[gunLevel - 1];

        movementSpeed = tank.GetMovementSpeed()[tankLevel - 1];
        rotationSpeed = tank.GetRotationSpeed()[tankLevel - 1];

        baseMinDamage = turret.GetMinDamage()[gunLevel - 1];
        baseMaxDamage = turret.GetMaxDamage()[gunLevel - 1];
        minDamage = baseMinDamage;
        maxDamage = baseMaxDamage;

        bulletSpeed = turret.GetBulletSpeed()[gunLevel - 1];
        bulletLifetime = turret.GetLifetime();
        bulletKnockback = turret.GetKnockBack();

        objBullet = turret.GetBulletType();

        playerAbilities.OnTriggerEnergyShield += HandleToggleEnergyShield;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        RegenerateHealth(deltaTime);
        HandleDamageBoost(deltaTime);

        if (energyShieldEnabled) { return; } // Using Energy shield does not regenerate energy

        RegenerateEnergy(deltaTime);

    }

    private void RegenerateHealth(float deltaTime)
    {
        if (healthSystem.GetAmount() == 0) { return; } // IF alive

        timeElapsedHealth += deltaTime;

        if (timeElapsedHealth < timePerHealth) { return; }

        healthSystem.Heal(1);
        timeElapsedHealth = 0;
    }

    private void RegenerateEnergy(float deltaTime)
    {
        timeElapsedEnergy += deltaTime;

        if (timeElapsedEnergy < timePerEnergy) { return; }

        energySystem.Heal(1);
        timeElapsedEnergy = 0;
    }

    private void UpdateAmmoUI()
    {
        OnAmmoUpdated?.Invoke(currentAmmoCount, maxAmmoCount);
    }

    private void UpdateStats()
    {
        // Update HP, Damage, Speed, etc. based on upgrades equipped. Run when confirming upgrades.
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

        GameManager.Instance.HandlePlayerDeath();
        gameObject.SetActive(false);
    }

    public bool TrySpendEnergy(int energy)
    {
        if (energySystem.GetAmount() < energy)
        {
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
    }

    public void SpendGold(int goldUsed)
    {
        goldSystem.TrySpendGold(goldUsed);
    }

    public int DealDamage()
    {
        return UnityEngine.Random.Range(minDamage, maxDamage + 1);
    }

    private void HandleDamageBoost(float deltaTime)
    {
        if (!incendiaryAmmoEnabled) { return; }

        damageBoostDuration = Mathf.Max(damageBoostDuration - deltaTime, 0f);
        TryRemoveDamageBoost();
    }

    public void AddDamageBoost(float percentage, float duration)
    {
        minDamage = (int)(minDamage * percentage);
        maxDamage = (int)(maxDamage * percentage);
        damageBoostDuration = duration;
        incendiaryAmmoEnabled = true;
    }

    private void TryRemoveDamageBoost()
    {
        if (damageBoostDuration != 0f || !incendiaryAmmoEnabled) { return; }

        minDamage = baseMinDamage;
        maxDamage = baseMaxDamage;
        incendiaryAmmoEnabled = false;
    }

    public void TankLevelUp()
    {
        if (tankLevel == TANK_MAX_LEVEL_LIMIT) { return; }

        tankLevel++;

        OnTankLeveledUp?.Invoke(tankLevel);
    }

    public void GunLevelUp()
    {
        if (gunLevel == TANK_MAX_LEVEL_LIMIT) { return; }

        gunLevel++;

        OnGunLeveledUp?.Invoke(gunLevel);
    }

    public void SetIsImmuned(bool value)
    {
        isImmuned = value;
    }

    private void HandleToggleEnergyShield()
    {
        energyShieldEnabled = !energyShieldEnabled;

        if (energyShieldEnabled) { return; }
        OnEnergyShieldDisabled?.Invoke();
    }

    private void OnDestroy()
    {
        playerAbilities.OnTriggerEnergyShield -= HandleToggleEnergyShield;
    }

    #region Stats Retrieving

    public HealthOrManaSystem GetHealthSystem() => healthSystem;
    public HealthOrManaSystem GetEnergySystem() => energySystem;

    public GoldSystem GetGoldSystem() => goldSystem;

    public string GetTankName() => tankName;
    public string GetGunName() => gunName;
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

    public ObjPlayerBullet GetBulletType() => objBullet;

    #endregion
}
