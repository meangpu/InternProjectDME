using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private ObjPlayerTank ObjPlayerTank = null;
    [SerializeField] private ObjTankTurret turret = null;
    

    // Misc
    private PlayerAbilities playerAbilities;
    private PlayerMovement playerMovement; 
    private PlayerGun playerGun;


    // Player ObjPlayerTank States
    
    private bool isDashing = false; // Check if the player is dashing
    private bool canDash = true; // Check if the player can dash

    private void Awake()
    {
        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GetComponent<PlayerMovement>();
        playerGun = GetComponent<PlayerGun>();
        
        if (TankCustomizationData.playerTank != null)
        {
            ObjPlayerTank = TankCustomizationData.playerTank;
        }
        if (TankCustomizationData.playerTurret != null)
        {
            turret = TankCustomizationData.playerTurret;
        }
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
        // Debug.Log("Performed an alternate attack");
        playerGun.ShootSpecial();
    }

    public void Skill1Activate()
    {
        if (!canDash) { return; }
        canDash = false;
        isDashing = true;
        playerAbilities.Dash(() => { isDashing = false; }, () => { canDash = true; });
    }

    public void Skill2Activate()
    {
        playerAbilities.Bomb();
    }

    public ObjPlayerTank GetTank() => ObjPlayerTank;
    public ObjTankTurret GetTurret() => turret;
}