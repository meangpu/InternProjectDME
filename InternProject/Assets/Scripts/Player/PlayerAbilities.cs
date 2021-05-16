using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private PlayerInputManager playerInput = null; // Change to input later

    [Header("Abilities parameters")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private Animator anim;

    private bool canDash = false; // Check if the player can dash

    private Action dashingCallback;
    private Action canDashCallback;
    
    public void Dash(Action dashingCallback, Action canDashCallback)
    {
        if (canDash) { return; }

        this.dashingCallback = dashingCallback;
        this.canDashCallback = canDashCallback;
        canDash = true;
        playerInput.DisableMovement();
        playerInput.DisableRotation();
        rb.velocity = (Vector2)transform.up * -dashSpeed;
        anim.SetTrigger("dash");
        StartCoroutine(OnDashCooldown());
    }

    private IEnumerator OnDashCooldown()
    {
        yield return new WaitForSeconds(dashDuration);
        dashingCallback();
        playerInput.EnableMovement();
        playerInput.EnableRotation();
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        canDash = false;
        canDashCallback();
    }

    public void CastEnergyShield()
    {

    }

    public void LaunchHomingMissile()
    {

    }
}
