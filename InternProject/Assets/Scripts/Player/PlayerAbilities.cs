using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private PlayerEquippedAddons playerEquippedAddons = null;
    [SerializeField] private CooldownSystem cooldownSystem = null;
    [SerializeField] private ObjAbility energyShieldAddon = null;

    /*[Header("Dash Ability parameters")]
    [SerializeField] private int dashEnergyCost = 20;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private float dashDuration = 0.25f;*/
    [SerializeField] private Animator anim;

    /*[Header("Bomb Ability parameters")]
    [SerializeField] private int bombEnergyCost = 45;
    [SerializeField] private int bombDamage = 15;
    [SerializeField] private float bombRange = 3.5f;
    [SerializeField] private float bombCooldown = 25f;*/

    /*private bool canDash = true; // Check if the player can dash
    private bool bombOnCooldown = false;*/

    // ------------------- Events -----------------------
    // Events that interfere the movements
    public event Action OnStartedDashing;
    public event Action OnFinishedDashing;

    public event Action OnTriggerEnergyShield;

    // ------------------- Events -----------------------

    private PlayerStats playerStats;
    private List<HotkeyAbility> hotkeyAbilityList;

    public class HotkeyAbility
    {
        public ObjAbility addon;
        public Action activateAbilityAction;
    }

    private void Awake()
    {
        List<ObjAbility> addonsList = playerEquippedAddons.GetEquippedAddons();
        ObjAbility addonQ = addonsList[0];
        ObjAbility addonE = addonsList[1];

        hotkeyAbilityList = new List<HotkeyAbility>
        {
            new HotkeyAbility
            {
                addon = addonQ,
                activateAbilityAction = () => ActivateAbility(addonQ)
            },

            new HotkeyAbility
            {
                addon = addonE,
                activateAbilityAction = () => ActivateAbility(addonE)
            }
        };
    }

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        if (!playerEquippedAddons.IsEnergyShieldEquipped()) { return; }

        playerStats.OnEnergyShieldDisabled += PutEnergyShieldOnCooldown;
    }

    private void ActivateAbility(ObjAbility ability)
    {
        AbilityType abilityType = ability.GetAbilityType();
        int energyCost = ability.GetEnergyCost();

        if (cooldownSystem.IsOnCooldown(abilityType)) { return; }

        if (!playerStats.TrySpendEnergy(energyCost)) { return; }

        switch (abilityType)
        {
            case AbilityType.Empty:
                Debug.Log("THIS IS EMPTY");
                break;
            case AbilityType.AutoLoader:
                Debug.Log($"Cooldown: {ability.GetCooldown()} Cost: {ability.GetEnergyCost()}");
                break;
            case AbilityType.Bomb:
                Bomb(ability.GetRange(), ability.GetDamage());
                break;
            case AbilityType.Dash:
                Dash(ability.GetRange(), ability.GetDuration());
                break;
            case AbilityType.Electrocharge:
                Debug.Log($"Cooldown: {ability.GetCooldown()} Cost: {ability.GetEnergyCost()}");
                break;
            case AbilityType.EnergyOrb:
                Debug.Log($"Cooldown: {ability.GetCooldown()} Cost: {ability.GetEnergyCost()}");
                break;
            case AbilityType.EnergyShield:
                ActivateEnergyShield();
                break;
            case AbilityType.HomingMissile:
                Debug.Log($"Cooldown: {ability.GetCooldown()} Cost: {ability.GetEnergyCost()}");
                break;
            case AbilityType.IncendiaryAmmo:
                Debug.Log($"Cooldown: {ability.GetCooldown()} Cost: {ability.GetEnergyCost()}");
                break;
        }

        switch (abilityType)
        {
            default:
                cooldownSystem.PutOnCooldown(ability);
                break;
            case AbilityType.EnergyShield:
                break;
        }
    }

    private void PutEnergyShieldOnCooldown()
    {
        cooldownSystem.PutOnCooldown(energyShieldAddon);
    }

    private void OnDestroy()
    {
        if (!playerEquippedAddons.IsEnergyShieldEquipped()) { return; }

        playerStats.OnEnergyShieldDisabled -= PutEnergyShieldOnCooldown;
    }

    

    #region For Inputs
    public void Skill1Activate()
    {
        hotkeyAbilityList[0].activateAbilityAction();
    }

    public void Skill2Activate()
    {
        hotkeyAbilityList[1].activateAbilityAction();
    }
    #endregion

    #region Abilities

    public void Dash(float speed, float duration)
    {
        /*if (!canDash) { return; }

        if (!playerStats.TrySpendEnergy(dashEnergyCost)) { return; }*/

        OnStartedDashing?.Invoke();
        //canDash = false;
        rb.velocity = (Vector2)transform.up * -speed;
        anim.SetTrigger("dash");
        StartCoroutine(OnDashCooldown(duration));
    }

    private IEnumerator OnDashCooldown(float dashDuration)
    {
        yield return new WaitForSeconds(dashDuration);
        OnFinishedDashing?.Invoke();
        /*yield return new WaitForSeconds(dashCooldown - dashDuration);
        canDash = true;*/
    }

    public void Bomb(float range, int damage)
    {
        // if (bombOnCooldown) { return; }

        // if (!playerStats.TrySpendEnergy(bombEnergyCost)) { return; }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);

        CinemacineShake.Instance.ShakeCam(7f, 0.514f); // shake screen

        foreach (Collider2D collider in enemies)
        {
            if (collider.TryGetComponent(out EnemyGetHit enemy))
            {
                enemy.TakeDamage(damage);
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

    #endregion
}
