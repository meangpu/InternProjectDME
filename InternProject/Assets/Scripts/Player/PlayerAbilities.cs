using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private PlayerEquippedAddons playerEquippedAddons = null;
    [SerializeField] private CooldownSystem cooldownSystem = null;
    [SerializeField] private ObjAbility energyShieldAddon = null;
    [SerializeField] private PlayerGun playerGun = null;
    [SerializeField] private GameObject orb = null;

    [SerializeField] private Animator anim;

    // ------------------- Events -----------------------
    // Events that interfere the movements
    public event Action OnStartedDashing;
    public event Action OnFinishedDashing;

    public event Action OnTriggerEnergyShield;

    public UnityEvent OnDashActivated;
    public UnityEvent OnAutoLoaderActivated;
    public UnityEvent OnBombActivated;
    public UnityEvent OnElectrochargeActivated;
    public UnityEvent OnHomingMissileActivated;
    public UnityEvent OnEnergyOrbActivated;
    public UnityEvent OnIncendiaryActivated;
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

        playerStats.OnPlayerRespawned += HandleRespawn;

        if (!playerEquippedAddons.IsEnergyShieldEquipped()) { return; }

        playerStats.OnEnergyShieldDisabled += PutEnergyShieldOnCooldown;
    }

    private void ActivateAbility(ObjAbility ability)
    {
        AbilityType abilityType = ability.GetAbilityType();
        int energyCost = ability.GetEnergyCost();
        ComboType comboType = playerEquippedAddons.GetComboType();

        if (cooldownSystem.IsOnCooldown(abilityType)) { return; }

        if (!playerStats.TrySpendEnergy(energyCost)) { return; }

        switch (abilityType)
        {
            case AbilityType.Empty:
                break;
            case AbilityType.AutoLoader:
                FastReload(ability.GetPercentage());
                break;
            case AbilityType.Bomb:
                Bomb(comboType, ability.GetRange(), ability.GetDamage(), ability.GetComboValue());
                break;
            case AbilityType.Dash:
                Dash(comboType, ability.GetRange(), ability.GetDuration());
                break;
            case AbilityType.Electrocharge:
                ActivateElectrocharge(ability.GetRange(), ability.GetDuration(), ability.GetPercentage());
                break;
            case AbilityType.EnergyOrb:
                ActivateEnergyOrb(comboType, ability.GetDamage(), ability.GetRange(), ability.GetDuration(), ability.GetComboValue());
                break;
            case AbilityType.EnergyShield:
                ActivateEnergyShield();
                break;
            case AbilityType.HomingMissile:
                LaunchHomingMissile(comboType, ability.GetDamage(), ability.GetRange(), ability.GetDuration(), ability.GetComboValue());
                break;
            case AbilityType.IncendiaryAmmo:
                ActivateIncendiary(comboType, ability.GetDuration(), ability.GetPercentage(), ability.GetComboValue());
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

    private void HandleRespawn()
    {
        cooldownSystem.PutOnCooldown(hotkeyAbilityList[0].addon);
        cooldownSystem.PutOnCooldown(hotkeyAbilityList[1].addon);
    }

    private void OnDestroy()
    {
        playerStats.OnPlayerRespawned -= HandleRespawn;

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

    private void Dash(ComboType comboType, float speed, float duration)
    {
        OnDashActivated?.Invoke();
        OnStartedDashing?.Invoke();
        rb.velocity = (Vector2)transform.up * -speed;
        anim.SetTrigger("dash");
        StartCoroutine(OnDash(comboType, duration));

        if (comboType != ComboType.EnergyDash) { return; }

        playerStats.SetIsImmuned(true);
    }

    private IEnumerator OnDash(ComboType comboType, float dashDuration)
    {
        yield return new WaitForSeconds(dashDuration);
        OnFinishedDashing?.Invoke();

        rb.angularVelocity = 0;

        if (comboType != ComboType.EnergyDash) { yield break; }

        playerStats.SetIsImmuned(false);
    }

    private void Bomb(ComboType comboType, float range, int damage, float comboRange)
    {
        OnBombActivated?.Invoke();
        bool isEMP = false;

        if (comboType != ComboType.UpgradedMissile) // Avoid conflicts with Homing Missile + Bomb combo
        {
            isEMP = comboType == ComboType.EMP;
            range = isEMP ? comboRange : range;
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);

        CinemacineShake.Instance.ShakeCam(7f, 0.514f); // shake screen

        foreach (Collider2D collider in enemies)
        {
            if (collider.TryGetComponent(out IEnemy enemy))
            {
                enemy.TakeDamage(damage);

                if (!isEMP) { continue; }

                enemy.Stun();
            }
        }
    }

    private void LaunchHomingMissile(ComboType comboType, int damage, float range, float duration, float comboValue)
    {
        OnHomingMissileActivated?.Invoke();

        range = comboType == ComboType.UpgradedMissile ? comboValue : range;

        PoolingSingleton.Instance.HomingMissilePool.SpawnPlayerMissile(transform.position, transform.rotation, damage, playerStats.GetBulletSpeed(), range, duration);
    }

    private void ActivateEnergyShield()
    {
        OnTriggerEnergyShield?.Invoke();
    }

    private void FastReload(float percentage)
    {
        OnAutoLoaderActivated?.Invoke();
        StartCoroutine(playerGun.Reload(percentage));
    }

    private void ActivateIncendiary(ComboType comboType, float duration, float percentage, float comboValue)
    {
        OnIncendiaryActivated?.Invoke();

        percentage = comboType == ComboType.IncendiaryCharge ? comboValue : percentage;

        float normalizedPercentage = (100 + percentage) / 100;

        playerStats.AddDamageBoost(normalizedPercentage, duration);
    }

    private void ActivateElectrocharge(float range, float duration, float percentage)
    {
        OnElectrochargeActivated?.Invoke();

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D collider in enemies)
        {
            if (collider.TryGetComponent(out IEnemy enemy))
            {
                enemy.Slow(percentage, duration);
            }
        }
    }

    private void ActivateEnergyOrb(ComboType comboType, int damage, float range, float duration, float comboValue)
    {
        OnEnergyOrbActivated?.Invoke();
        int energyPerHit = comboType == ComboType.EnergyDrainOrb ? (int)comboValue : 0;

        orb.SetActive(true);
        orb.GetComponent<EnergyOrb>().Setup(damage, range, duration, energyPerHit);
    }
    #endregion
}
