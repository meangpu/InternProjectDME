using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    private ObjPlayerTank tank = null;
    private ObjTankTurret turret = null;
    [SerializeField] PlayerTankCustomization scriptObjDataTankGun;
    
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

        tank = scriptObjDataTankGun.GetTank();
        turret = scriptObjDataTankGun.GetTurret();
        

    }

    private void Start()
    {
        playerAbilities.OnStartedDashing += HandleOnDashed;
        playerAbilities.OnFinishedDashing += HandleAfterDashed;
        UIManager.Instance.customStart();
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

    public ObjPlayerTank GetTank() => tank;
    public ObjTankTurret GetTurret() => turret;

    private void OnDestroy()
    {
        playerAbilities.OnStartedDashing -= HandleOnDashed;
        playerAbilities.OnFinishedDashing -= HandleAfterDashed;
    }
}