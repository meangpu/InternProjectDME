using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Transform barrel = null; // Bullet Spawn point
    [SerializeField] private Tank tank = null;

    // Misc
    private UIManager uiManager;
    private Pooler bulletPool;
    private PlayerAbilities playerAbilities;
    private PlayerInputManager input;
    private PlayerAimAtPoint gun;

    // Player Controls vars
   
    private float moveDirection;
    private float rotateDirection;
    private Vector2 mousePos;

    // Player Tank States
    private bool canShoot = true; // Check if the player can shoot between shots
    private bool holdOnShoot = false;  // Check if the player is holding down shoot button to continuously shoot.
    private bool isReloading = false; // Check if the player is reloading to prevent ghost reloads.
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
        bulletPool = GameObject.Find("PlayerBulletPooler").GetComponent<Pooler>();
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
        
        if (holdOnShoot) { Shoot(); } // Shoot continuously while shoot button is held down.
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

    public void OnHoldShootButton() // If shoot button is held down.
    {
        holdOnShoot = true;
    }

    public void OnReleaseShootButton() // If shoot button is released.
    {
        holdOnShoot = false;
    }

    private void Shoot() 
    {
        if (!canShoot) { return; }

        if (currentAmmoCount < 1) // If ammo is depleted and the player attempts to shoot, do an auto reload.
        {
            StartCoroutine(Reload());
        }
        else // Shoot normally
        {
            bulletPool.SpawnObject(barrel.position, barrel.rotation);
            StartCoroutine(StartShootCooldown(cooldownBetweenShots));
        }  
    }

    private IEnumerator StartShootCooldown(float cooldownTime)
    {
        currentAmmoCount--;
        UpdateAmmoUI();
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    
    public void SpecialShoot()
    {
        Debug.Log("Performed an alternate attack");
    }

    public IEnumerator Reload()
    {
        if (isReloading) { yield break; } // in IEnumerator, yield break = return;

        isReloading = true;
        currentAmmoCount = 0;
        UpdateAmmoUI();
        yield return new WaitForSeconds(reloadTime);
        currentAmmoCount = maxAmmoCount;
        UpdateAmmoUI();
        isReloading = false;
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

    private void UpdateAmmoUI()
    {
        uiManager.UpdateAmmoUI(currentAmmoCount, maxAmmoCount);
    }
}