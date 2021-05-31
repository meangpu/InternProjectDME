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
    [SerializeField] private PlayerGun playerGun = null;

    [SerializeField] private Animator anim;

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
                activateAbilityAction = () => ActivateAbility(addonQ, PlayerEquippedAddons.AddonSlot.SlotQ)
            },

            new HotkeyAbility
            {
                addon = addonE,
                activateAbilityAction = () => ActivateAbility(addonE, PlayerEquippedAddons.AddonSlot.SlotQ)
            }
        };
    }

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        if (!playerEquippedAddons.IsEnergyShieldEquipped()) { return; }

        playerStats.OnEnergyShieldDisabled += PutEnergyShieldOnCooldown;
    }

    private void ActivateAbility(ObjAbility ability, PlayerEquippedAddons.AddonSlot slot)
    {
        AbilityType abilityType = ability.GetAbilityType();
        int energyCost = ability.GetEnergyCost();
        bool isCombo = playerEquippedAddons.IsCombo;

        if (cooldownSystem.IsOnCooldown(abilityType)) { return; }

        if (!playerStats.TrySpendEnergy(energyCost)) { return; }

        switch (abilityType)
        {
            case AbilityType.Empty:
                Debug.Log("THIS IS EMPTY");
                break;
            case AbilityType.AutoLoader:
                FastReload(ability.GetPercentage());
                break;
            case AbilityType.Bomb:
                Bomb(isCombo, ability.GetRange(), ability.GetDamage());
                break;
            case AbilityType.Dash:
                Dash(isCombo, ability.GetRange(), ability.GetDuration());
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

    private void Dash(bool isCombo, float speed, float duration)
    {
        OnStartedDashing?.Invoke();
        rb.velocity = (Vector2)transform.up * -speed;
        anim.SetTrigger("dash");
        StartCoroutine(OnDash(isCombo, duration));

        if (!isCombo) { return; }

        playerStats.SetIsImmuned(true);
        Debug.Log("PHASING");
    }

    private IEnumerator OnDash(bool isCombo, float dashDuration)
    {
        yield return new WaitForSeconds(dashDuration);
        OnFinishedDashing?.Invoke();

        if (!isCombo) { yield break; }

        playerStats.SetIsImmuned(false);
    }

    private void Bomb(bool isCombo, float range, int damage)
    {
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

    private void LaunchHomingMissile()
    {
        // Summon Homing Missile
    }

    private void ActivateEnergyShield()
    {
        OnTriggerEnergyShield?.Invoke();
    }

    private void FastReload(float percentage)
    {
        StartCoroutine(playerGun.Reload(percentage));
    }
    #endregion
}
