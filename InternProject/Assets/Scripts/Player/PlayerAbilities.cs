using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Player player = null; // Change to input later

    [Header("Abilities parameters")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private float dashDuration = 0.25f;

    private bool isDashing = false; // Check if the player is dashing

    public void Dash()
    {
        if (isDashing) { return; }

        isDashing = true;
        player.DisableMovement();
        player.DisableRotation();
        rb.velocity = (Vector2)transform.up * dashSpeed;
        StartCoroutine(OnDashCooldown());
    }

    private IEnumerator OnDashCooldown()
    {
        yield return new WaitForSeconds(dashDuration);
        player.EnableMovement();
        player.EnableRotation();
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        isDashing = false;
    }

    public void CastEnergyShield()
    {

    }

    public void LaunchHomingMissile()
    {

    }
}
