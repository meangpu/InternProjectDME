using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private ObjPlayerTank tank = null;
    [SerializeField] private ObjTankTurret turret = null;
    
    // Misc
    private PlayerAbilities playerAbilities;
    private PlayerMovement playerMovement; 
    private PlayerGun playerGun;

    // Player Tank States 
    private bool isDashing = false; // Check if the player is dashing

    private void Awake()
    {
        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GetComponent<PlayerMovement>();
        playerGun = GetComponent<PlayerGun>();
        
        if (TankCustomizationData.playerTank != null)
        {
            tank = TankCustomizationData.playerTank;
        }
        if (TankCustomizationData.playerTurret != null)
        {
            turret = TankCustomizationData.playerTurret;
        }
    }

    private void Start()
    {
        playerAbilities.OnStartedDashing += HandleOnDashed;
        playerAbilities.OnFinishedDashing += HandleAfterDashed;
    }

    private void Update()
    {
        if (isDashing) { return; }
 
        playerMovement.Move();
        playerMovement.RotateTank();
    }

    private void HandleOnDashed()
    {
        isDashing = true;
    }

    private void HandleAfterDashed()
    {
        isDashing = false;
    }

    public void SpecialShoot()
    {
        playerGun.ShootSpecial();
    }

    public ObjPlayerTank GetTank() => tank;
    public ObjTankTurret GetTurret() => turret;

    private void OnDestroy()
    {
        playerAbilities.OnStartedDashing -= HandleOnDashed;
        playerAbilities.OnFinishedDashing -= HandleAfterDashed;
    }
}