using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    // Player Stats
    private readonly int startingGold = 500;
    private int tankLevel = 1;

    private GoldSystem goldSystem;

    // Health and Energy
    private HealthOrManaSystem healthSystem;
    private HealthOrManaSystem energySystem;

    // Tank Stats from Scriptable Object
    private string tankName;
    private int minDamage;
    private int maxDamage;
    private float energyRegenRate;
    private float timePerEnergy;
    private float cooldownBetweenShots;
    private float fireRate;
    private int maxAmmoCount;
    private int currentAmmoCount;
    private float reloadTime;
    private float movementSpeed;
    private float rotationSpeed;

    private float timeElapsed;

    private const int TANK_MAX_LEVEL_LIMIT = 3;

    private TankTurret turret;
    private Tank tank;

    public event Action<int, int> OnAmmoUpdated;
    public event Action<int> OnTankLeveledUp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Player player = GetComponent<Player>();

        turret = player.GetTurret();
        tank = player.GetTank();
    }

    private void Start()
    {   
        healthSystem = new HealthOrManaSystem(tank.GetHealth());
        energySystem = new HealthOrManaSystem(tank.GetEnergy());

        goldSystem = new GoldSystem(startingGold);

        tankName = tank.GetName();

        energyRegenRate = tank.GetEnergyRate();
        timePerEnergy = 1 / energyRegenRate;

        fireRate = turret.GetRateOfFire();
        cooldownBetweenShots = 1 / fireRate;

        maxAmmoCount = turret.GetAmmoCount();
        currentAmmoCount = maxAmmoCount;

        reloadTime = turret.GetReloadTime();

        movementSpeed = tank.GetMovementSpeed();
        rotationSpeed = tank.GetRotationSpeed();

        minDamage = turret.GetMinDamage();
        maxDamage = turret.GetMaxDamage();
    }

    private void Update()
    {
        RegenerateEnergy();
    }

    private void RegenerateEnergy()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timePerEnergy)
        {
            energySystem.Heal(1);
            timeElapsed = 0;
        }
    }

    private void UpdateAmmoUI()
    {
        OnAmmoUpdated?.Invoke(currentAmmoCount, maxAmmoCount);
    }

    private void UpdateStats()
    {
        // Update HP, Damage, Speed, etc. based on upgrades equipped. Run when confirming upgrades.
    }

    public bool SpendEnergy(int energy)
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

    public int DealDamage()
    {
        return UnityEngine.Random.Range(minDamage, maxDamage + 1);
    }

    public void LevelUp()
    {
        if (tankLevel == TANK_MAX_LEVEL_LIMIT)
        {
            return;
        }

        tankLevel += 1;

        OnTankLeveledUp?.Invoke(tankLevel);
    }

    #region Stats Retrieving

    public HealthOrManaSystem GetHealthSystem() => healthSystem;
    public HealthOrManaSystem GetEnergySystem() => energySystem;

    public GoldSystem GetGoldSystem() => goldSystem;

    public string GetTankName() => tankName;
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

    #endregion
}
