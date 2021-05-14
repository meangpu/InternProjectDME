using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    [Header("Abilities parameters")]
    [SerializeField] private float dashSpeed = 10f;

    private bool isDashing = false; // Check if the player is dashing

    public void Dash()
    {
        if (isDashing) { return; }

        isDashing = true;
        rb.velocity = (Vector2)transform.up * dashSpeed;
    }

    public void CastEnergyShield()
    {

    }

    public void LaunchHomingMissile()
    {

    }
}
