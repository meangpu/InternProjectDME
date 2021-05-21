using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar = null;
    [SerializeField] private Image healthBarDamaged = null;
    [SerializeField] private TMP_Text healthText = null;

    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 0.5f;

    private PlayerStats playerStats;
    private float damagedHealthShrinkTimer;

    private void Awake()
    {
        playerStats = PlayerStats.Instance;
    }

    private void Start()
    {   
        HandleHealTaken(playerStats.GetHealth(), playerStats.GetMaxHealth());
        playerStats.OnDamageTaken += HandleDamageTaken;
        playerStats.OnHealTaken += HandleHealTaken;
    }

    private void Update()
    {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0)
        {
            if (healthBar.fillAmount < healthBarDamaged.fillAmount)
            {
                float shrinkSpeed = 0.5f;
                healthBarDamaged.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    private void UpdateUI(int currentHealth, int maxHealth)
    {
        healthText.text = $"{currentHealth} / {maxHealth}";

        float healthPercentage = (float)currentHealth / maxHealth;

        healthBar.fillAmount = healthPercentage;
    }

    private void HandleDamageTaken(int currentHealth, int maxHealth) 
    {
        UpdateUI(currentHealth, maxHealth);
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
    }

    private void HandleHealTaken(int currentHealth, int maxHealth)
    {
        UpdateUI(currentHealth, maxHealth);

        healthBarDamaged.fillAmount = healthBar.fillAmount;
    }

    private void OnDestroy()
    {
        playerStats.OnDamageTaken -= HandleDamageTaken;
        playerStats.OnHealTaken -= HandleHealTaken;
    }
}
