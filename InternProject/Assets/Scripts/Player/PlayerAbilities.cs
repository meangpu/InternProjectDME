using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    [Header("Dash Ability parameters")]
    [SerializeField] private int dashEnergyCost = 20;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private Animator anim;

    [Header("Bomb Ability parameters")]
    [SerializeField] private int bombEnergyCost = 45;
    [SerializeField] private int bombDamage = 15;
    [SerializeField] private float bombRange = 3.5f;
    [SerializeField] private float bombCooldown = 25f;

    private bool canDash = true; // Check if the player can dash
    private bool bombOnCooldown = false;

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
        Electrocharge,
        Bomb,
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

    public void Bomb()
    {
        if (bombOnCooldown) { return; }

        if (!playerStats.SpendEnergy(bombEnergyCost)) { return; }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRange);

        foreach (Collider2D collider in enemies)
        {
            if (collider.TryGetComponent(out EnemyGetHit enemy))
            {
                enemy.TakeDamage(bombDamage);
            }
        }
    }

    public void LaunchHomingMissile()
    {

    }
}
