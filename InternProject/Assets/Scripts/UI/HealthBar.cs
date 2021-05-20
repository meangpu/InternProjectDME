using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar = null;
    [SerializeField] private TMP_Text healthText = null;

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = PlayerStats.Instance;
    }

    private void Start()
    {   
        UpdateUI(playerStats.GetHealth(), playerStats.GetMaxHealth());
        playerStats.OnDamageTaken += UpdateUI;
        playerStats.OnHealTaken += UpdateUI;
    }

    private void UpdateUI(int currentHealth, int maxHealth)
    {
        healthText.text = $"{currentHealth} / {maxHealth}";

        float healthPercentage = (float)currentHealth / maxHealth;

        healthBar.fillAmount = healthPercentage;
    }

    private void OnDestroy()
    {
        playerStats.OnDamageTaken -= UpdateUI;
        playerStats.OnHealTaken -= UpdateUI;
    }
}
