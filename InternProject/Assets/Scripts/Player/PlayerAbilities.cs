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

    // ------------------- Events -----------------------
    // Events that interfere the movements
    public event Action OnStartedDashing;
    public event Action OnFinishedDashing;

    public event Action OnTriggerEnergyShield;

    // ------------------- Events -----------------------

    private PlayerStats playerStats;
    private List<HotkeyAbility> hotkeyAbilityList;

    private void Awake()
    {
        playerStats = PlayerStats.Instance;
        hotkeyAbilityList = new List<HotkeyAbility>
        {
            new HotkeyAbility
            {
                abilityType = AbilityType.Dash,
                activateAbilityAction = () => Dash()
            },

            new HotkeyAbility
            {
                abilityType = AbilityType.EnergyShield,
                activateAbilityAction = () => ActivateEnergyShield()
            }
        };
    }

    public enum AbilityType
    {
        Empty,
        Dash,
        EnergyShield,
        EnergyOrb,
        HomingMissile,
        Electrocharge,
        Bomb,
        IncendiaryAmmo,
        AutoLoader,
    }

    public void Skill1Activate()
    {
        hotkeyAbilityList[0].activateAbilityAction();
    }

    public void Skill2Activate()
    {
        hotkeyAbilityList[1].activateAbilityAction();
    }

    public void Dash()
    {
        if (!canDash) { return; }

        if (!playerStats.SpendEnergy(dashEnergyCost)) { return; }

        OnStartedDashing?.Invoke();
        canDash = false;
        rb.velocity = (Vector2)transform.up * -dashSpeed;
        anim.SetTrigger("dash");
        StartCoroutine(OnDashCooldown());
    }

    private IEnumerator OnDashCooldown()
    {
        yield return new WaitForSeconds(dashDuration);
        OnFinishedDashing?.Invoke();
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        canDash = true;
    }

    public void Bomb()
    {
        if (bombOnCooldown) { return; }

        if (!playerStats.SpendEnergy(bombEnergyCost)) { return; }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRange);

        CinemacineShake.Instance.ShakeCam(7f, 0.514f); // shake screen

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
        // Summon Homing Missile
    }

    public void ActivateEnergyShield()
    {
        OnTriggerEnergyShield?.Invoke();
    }

    public class HotkeyAbility
    {
        public AbilityType abilityType;
        public Action activateAbilityAction;
    }
}
