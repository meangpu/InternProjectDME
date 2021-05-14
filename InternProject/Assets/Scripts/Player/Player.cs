using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Tank tank = null;

    // Misc
    private UIManager uiManager;
    private PlayerAbilities playerAbilities;
    private PlayerInputManager input;

    // Player Controls vars
    private float moveDirection;
    private float rotateDirection;

    // Player Tank States
    
    private bool isDashing = false; // Check if the player is dashing
    private bool canDash = true; // Check if the player can dash

    // Tank stats
    private float cooldownBetweenShots;
    private float fireRate;
    private float movementSpeed;
    private float rotationSpeed;
    private int maxAmmoCount;
    private int currentAmmoCount;
    private float reloadTime;

    private void Start()
    {
        input = GetComponent<PlayerInputManager>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        playerAbilities = GetComponent<PlayerAbilities>();

        fireRate = tank.rateOfFire;
        cooldownBetweenShots = 1 / fireRate;

        movementSpeed = tank.moveSpeed;
        rotationSpeed = tank.rotationSpeed;

        maxAmmoCount = tank.ammoCount;
        currentAmmoCount = maxAmmoCount;

        reloadTime = tank.reloadTime;

        UpdateAmmoUI();
    }


    private void Update()
    {
        ReadInputValues();

        if (!isDashing)
        {
            Move();
            RotateTank();
        }
    }

    private void ReadInputValues() // Read all input values from the Input System
    {
        moveDirection = input.GetMoveValue();
        rotateDirection = input.GetRotationValue();
    }

    private void Move()
    {
        rb.velocity = new Vector2(transform.up.x, transform.up.y) * -moveDirection * movementSpeed;
    }

    private void RotateTank()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -rotateDirection * rotationSpeed));
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