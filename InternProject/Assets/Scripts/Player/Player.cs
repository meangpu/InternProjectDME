using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Transform gun = null; // Gun Pivoting point
    [SerializeField] private Transform barrel = null; // Bullet Spawn point
    [SerializeField] private Tank tank = null;

    [Header("Temp Fields")]
    [SerializeField] private GameObject bulletPrefab = null;

    private Camera mainCamera;

    // Player Controls vars
    private PlayerControls playerControls;
    private float moveDirection;
    private float rotateDirection;
    private Vector2 mousePos;

    // Player Tank States
    private bool canShoot = true; // Check if the player can shoot between shots
    private bool holdOnShoot = false;  // Check if the player is holding down shoot button to continuously shoot.

    // Tank stats
    private float cooldownBetweenShots;
    private float fireRate;
    private float movementSpeed;
    private float rotationSpeed;
    private int maxAmmoCount;
    private int currentAmmoCount;
    private float reloadTime;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        mainCamera = Camera.main;

        playerControls.Tank.Shoot.performed += _ => OnHoldShootButton();
        playerControls.Tank.Shoot.canceled += _ => OnReleaseShootButton();
        playerControls.Tank.SpecialShoot.performed += _ => SpecialShoot();
        playerControls.Tank.Reload.performed += _ => StartCoroutine(Reload());
        playerControls.Tank.Skill1.performed += _ => Skill1Activate();

        fireRate = tank.rateOfFire;
        cooldownBetweenShots = 1 / fireRate;

        movementSpeed = tank.moveSpeed;
        rotationSpeed = tank.rotationSpeed;

        maxAmmoCount = tank.ammoCount;
        currentAmmoCount = maxAmmoCount;

        reloadTime = tank.reloadTime;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        ReadInputValues();
        RotateBarrel();
        Move();
        RotateTank();

        if (holdOnShoot) { Shoot(); } // Shoot continuously while shoot button is held down.
    }

    private void ReadInputValues() // Read all input values from the Input System
    {
        moveDirection = playerControls.Tank.Move.ReadValue<float>();
        rotateDirection = playerControls.Tank.Rotate.ReadValue<float>();
        mousePos = playerControls.Tank.LookAt.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.velocity = new Vector2(transform.up.x, transform.up.y) * -moveDirection * movementSpeed;
    }

    private void RotateTank()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -rotateDirection * rotationSpeed));
    }

    private void OnHoldShootButton() // If shoot button is held down.
    {
        holdOnShoot = true;
    }

    private void OnReleaseShootButton() // If shoot button is released.
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
            Instantiate(bulletPrefab, barrel.position, barrel.rotation);
            StartCoroutine(StartShootCooldown(cooldownBetweenShots));
        }  
    }

    private IEnumerator StartShootCooldown(float cooldownTime)
    {
        currentAmmoCount--;
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    private void RotateBarrel()
    {
        Vector2 cursorPosOnScreen = mainCamera.ScreenToWorldPoint(mousePos);

        Vector3 gunDirection = (Vector3)cursorPosOnScreen - gun.position;

        float aimAtAngle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;

        gun.rotation = Quaternion.RotateTowards(gun.rotation, Quaternion.Euler(0, 0, aimAtAngle+90), Mathf.Infinity);
    }

    private void SpecialShoot()
    {
        Debug.Log("Performed an alternate attack");
    }

    private IEnumerator Reload()
    {
        currentAmmoCount = 0;
        yield return new WaitForSeconds(reloadTime);
        currentAmmoCount = maxAmmoCount;
    }

    private void Skill1Activate()
    {
        Debug.Log("First ability slot activated");
    }

    private void OnStatsUpdate()
    {
        // Update HP, Damage, Speed, etc. based on upgrades equipped. Run when confirming upgrades.
    }
}