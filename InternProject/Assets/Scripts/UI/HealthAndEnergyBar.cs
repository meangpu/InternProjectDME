using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndEnergyBar : MonoBehaviour
{
    [SerializeField] private Image healthBar = null;
    [SerializeField] private Image healthBarDamaged = null;
    [SerializeField] private TMP_Text healthText = null;

    [SerializeField] private Image energyBar = null;
    [SerializeField] private Image energyBarSpent = null;
    [SerializeField] private TMP_Text energyText = null;

    [SerializeField] private float shrinkSpeed = 0.5f;
    [SerializeField] private const float BAR_SHRINK_TIMER_MAX = 0.5f;

    private PlayerStats playerStats;
    private float healthBarShrinkTimer;
    private float energyBarShrinkTimer;

    private HealthOrManaSystem healthSystem;
    private HealthOrManaSystem energySystem;

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        healthSystem = playerStats.GetHealthSystem();
        energySystem = playerStats.GetEnergySystem();
        
        SetBar(healthSystem.GetPercentage(), healthBar);
        SetBar(energySystem.GetPercentage(), energyBar);

        healthSystem.OnDamaged += HandleDamageTaken;
        healthSystem.OnHealed += HandleHealTaken;

        energySystem.OnDamaged += HandleEnergySpent;
        energySystem.OnHealed += HandleEnergyRegained;

        healthSystem.Heal(100, HealthOrManaSystem.HealingType.Percentage);
        energySystem.Heal(100, HealthOrManaSystem.HealingType.Percentage);
    }

    private void Update()
    {
        float timeElapsed = Time.deltaTime;
        healthBarShrinkTimer -= timeElapsed;
        energyBarShrinkTimer -= timeElapsed;

        if (healthBarShrinkTimer < 0)
        {
            if (healthBar.fillAmount < healthBarDamaged.fillAmount)
            {
                healthBarDamaged.fillAmount -= shrinkSpeed * timeElapsed;
            }
        }

        if (energyBarShrinkTimer < 0)
        {
            if (energyBar.fillAmount < energyBarSpent.fillAmount)
            {
                energyBarSpent.fillAmount -= shrinkSpeed * timeElapsed;
            }
        }
    }

    private void UpdateUI(TMP_Text textField, int currentHealth, int maxHealth)
    {
        textField.text = $"{currentHealth} / {maxHealth}";
    }

    private void HandleDamageTaken(int currentHealth, int maxHealth)
    {
        UpdateUI(healthText, currentHealth, maxHealth);
        SetBar(healthSystem.GetPercentage(), healthBar);
        healthBarShrinkTimer = BAR_SHRINK_TIMER_MAX;
    }

    private void HandleHealTaken(int currentHealth, int maxHealth)
    {
        UpdateUI(healthText, currentHealth, maxHealth);
        SetBar(healthSystem.GetPercentage(), healthBar);

        healthBarDamaged.fillAmount = healthBar.fillAmount;
    }

    private void HandleEnergySpent(int currentEnergy, int maxEnergy)
    {
        UpdateUI(energyText, currentEnergy, maxEnergy);
        SetBar(energySystem.GetPercentage(), energyBar);
        energyBarShrinkTimer = BAR_SHRINK_TIMER_MAX;
    }

    private void HandleEnergyRegained(int currentEnergy, int maxEnergy)
    {
        UpdateUI(energyText, currentEnergy, maxEnergy);
        SetBar(energySystem.GetPercentage(), energyBar);

        energyBarSpent.fillAmount = energyBar.fillAmount;
    }

    private void SetBar(float percentage, Image bar)
    {
        bar.fillAmount = percentage;
    }

    private void OnDestroy()
    {
        healthSystem.OnDamaged -= HandleDamageTaken;
        healthSystem.OnHealed -= HandleHealTaken;

        energySystem.OnDamaged -= HandleEnergySpent;
        energySystem.OnHealed -= HandleEnergyRegained;
    }
}

