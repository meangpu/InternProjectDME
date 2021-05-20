using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // public static PlayerStats Instance { get; private set; }

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

    private UIManager uiManager;
    private TankTurret turret;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        turret = GetComponent<Player>().GetTurret();
    }

    private void Start()
    {
        health = maxHealth;

        fireRate = turret.GetRateOfFire();
        cooldownBetweenShots = 1 / fireRate;

        maxAmmoCount = turret.GetAmmoCount();
        currentAmmoCount = maxAmmoCount;

        reloadTime = turret.GetReloadTime();

        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        uiManager.UpdateAmmoUI(currentAmmoCount, maxAmmoCount);
    }

    private void OnStatsUpdate()
    {
        // Update HP, Damage, Speed, etc. based on upgrades equipped. Run when confirming upgrades.
    }

    public int GetDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    public int GetMaxAmmoCount() => maxAmmoCount;
    public int GetCurrentAmmoCount() => currentAmmoCount;
    public void DecreaseCurrentAmmoCount(int decrement = 1) => currentAmmoCount -= decrement;
    public void EmptyCurrentAmmoCount() => currentAmmoCount = 0;
    public void ReloadAmmoCount() => currentAmmoCount = maxAmmoCount;

    public float GetCoolDownBetweenShots() => cooldownBetweenShots;

    public float GetReloadTime() => reloadTime;
}
