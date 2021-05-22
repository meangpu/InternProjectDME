using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    [Header("Abilities parameters")]
    [SerializeField] private int dashEnergyCost = 20;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private Animator anim;

    private bool canDash = true; // Check if the player can dash

    private Action dashingCallback;
    private Action canDashCallback;

    private PlayerStats playerStats;


    private void Awake()
    {
        playerStats = PlayerStats.Instance;
    }

    public enum AbilityType
    {
        Dash,
        EnergyShield,
        HomingMissile,
        Burrow,
        Electrocharge,
        Bomb,
        AmmoBox,
        IncendiaryAmmo,
        AutoLoader
    }

    public void Dash(Action dashingCallback, Action canDashCallback)
    {
        if (!canDash) { return; }

        if (!playerStats.SpendEnergy(dashEnergyCost)) { return; }

        this.dashingCallback = dashingCallback;
        this.canDashCallback = canDashCallback;
        canDash = false;
        rb.velocity = (Vector2)transform.up * -dashSpeed;
        anim.SetTrigger("dash");
        StartCoroutine(OnDashCooldown());
    }

    private IEnumerator OnDashCooldown()
    {
        yield return new WaitForSeconds(dashDuration);
        dashingCallback();
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        canDash = true;
        canDashCallback();
    }

    public void CastEnergyShield()
    {

    }

    public void LaunchHomingMissile()
    {

    }
}
