using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private TankTurret turret = null;

    // Misc
    private UIManager uiManager;
    private PlayerAbilities playerAbilities;
    private PlayerMovement playerMovement;

    // Player Tank States
    
    private bool isDashing = false; // Check if the player is dashing
    private bool canDash = true; // Check if the player can dash

    // Tank stats
    private float cooldownBetweenShots;
    private float fireRate;
    private int maxAmmoCount;
    private int currentAmmoCount;
    private float reloadTime;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        fireRate = turret.GetRateOfFire();
        cooldownBetweenShots = 1 / fireRate;

        maxAmmoCount = turret.GetAmmoCount();
        currentAmmoCount = maxAmmoCount;

        reloadTime = turret.GetReloadTime();

        UpdateAmmoUI();
    }


    private void Update()
    {
        if (!isDashing)
        {
            playerMovement.Move();
            playerMovement.RotateTank();
        }
    }

    public void SpecialShoot()
    {
        Debug.Log("Performed an alternate attack");
    }

    public void Skill1Activate()
    {
        if (!canDash) { return; }
        canDash = false;
        isDashing = true;
        playerAbilities.Dash(() => { isDashing = false; }, () => { canDash = true; });
    }

    private void OnStatsUpdate()
    {
        // Update HP, Damage, Speed, etc. based on upgrades equipped. Run when confirming upgrades.
    }

    public void UpdateAmmoUI()
    {
        uiManager.UpdateAmmoUI(currentAmmoCount, maxAmmoCount);
    }

    public int GetMaxAmmoCount() => maxAmmoCount;
    public int GetCurrentAmmoCount() => currentAmmoCount;
    public void DecreaseCurrentAmmoCount(int decrement = 1) => currentAmmoCount -= decrement;
    public void EmptyCurrentAmmoCount() => currentAmmoCount = 0;
    public void ReloadAmmoCount() => currentAmmoCount = maxAmmoCount;

    public float GetCoolDownBetweenShots() => cooldownBetweenShots;

    public float GetReloadTime() => reloadTime;
}