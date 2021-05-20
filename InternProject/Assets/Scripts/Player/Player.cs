using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Tank tank = null;
    [SerializeField] private TankTurret turret = null;

    // Misc
    private PlayerAbilities playerAbilities;
    private PlayerMovement playerMovement;

    // Player Tank States
    
    private bool isDashing = false; // Check if the player is dashing
    private bool canDash = true; // Check if the player can dash

    private void Awake()
    {
        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GetComponent<PlayerMovement>();
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

    public Tank GetTank() => tank;
    public TankTurret GetTurret() => turret;
}