using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    // Player Stats
    private int gold;
    private int tankLevel;

    // Tank Stats from Scriptable Object
    private int maxHealth;
    private int health;
    private int minDamage;
    private int maxDamage;
    private float cooldownBetweenShots;
    private float fireRate;
    private int maxAmmoCount;
    private int currentAmmoCount;
    private float reloadTime;
    private float movementSpeed;
    private float rotationSpeed;

    private TankTurret turret;
    private Tank tank;

    private void Awake()
    {
        Player player = GetComponent<Player>();

        turret = player.GetTurret();
        tank = player.GetTank();
    }

    private void Start()
    {
        maxHealth = tank.GetHealth();
        health = maxHealth;

        fireRate = turret.GetRateOfFire();
        cooldownBetweenShots = 1 / fireRate;

        maxAmmoCount = turret.GetAmmoCount();
        currentAmmoCount = maxAmmoCount;

        reloadTime = turret.GetReloadTime();

        movementSpeed = tank.GetMovementSpeed();
        rotationSpeed = tank.GetRotationSpeed();

        minDamage = turret.GetMinDamage();
        maxDamage = turret.GetMaxDamage();

        UpdateAmmoUI();
        UpdateHealthUI();
    }

    public void UpdateAmmoUI()
    {
        UIManager.Instance.UpdateAmmoUI(currentAmmoCount, maxAmmoCount);
    }

    public void UpdateHealthUI()
    {
        UIManager.Instance.UpdateHealthUI(health, maxHealth);
    }

    private void OnStatsUpdate()
    {
        // Update HP, Damage, Speed, etc. based on upgrades equipped. Run when confirming upgrades.
    }

    public int GetDamage()
    {
        return UnityEngine.Random.Range(minDamage, maxDamage + 1);
    }

    public void TakeDamage(int damageInflicted)
    {
        health -= damageInflicted;
        UpdateHealthUI();

        if (health <= 0)
        {
            Debug.Log("Player is dead");
        }
    }

    public int GetMaxAmmoCount() => maxAmmoCount;
    public int GetCurrentAmmoCount() => currentAmmoCount;
    public void DecreaseCurrentAmmoCount(int decrement = 1) => currentAmmoCount -= decrement;
    public void EmptyCurrentAmmoCount() => currentAmmoCount = 0;
    public void ReloadAmmoCount() => currentAmmoCount = maxAmmoCount;

    public float GetCoolDownBetweenShots() => cooldownBetweenShots;

    public float GetReloadTime() => reloadTime;

    public float GetMovementSpeed() => movementSpeed;
    public float GetRotationSpeed() => rotationSpeed;
}
