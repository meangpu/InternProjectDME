using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text ammoUI = null;
    [SerializeField] private ReloadBar reloadBar = null;
    [SerializeField] private TMP_Text tankNameText = null;
    [SerializeField] private TMP_Text tankLevelText = null;
    [SerializeField] private TMP_Text gunNameText = null;
    [SerializeField] private TMP_Text gunLevelText = null;
    [SerializeField] private Image respawnBar = null;
    [Header("BuyModeDisable")]
    [SerializeField] GameObject addOnDisplay;
    [SerializeField] GameObject bulletDisplay;
    [SerializeField] private GameObject upgradePanel = null;

    [Header("Upgrade Panels")]
    [SerializeField] private Image tankImage = null;
    [SerializeField] private Image gunImage = null;
    [SerializeField] private TMP_Text tankUpgradeLevelText = null;
    [SerializeField] private TMP_Text gunUpgradeLevelText = null;
    [SerializeField] private TMP_Text tankCostText = null;
    [SerializeField] private TMP_Text gunCostText = null;

    private PlayerStats playerStats;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        ResetRespawnBar();

        playerStats.OnAmmoUpdated += UpdateAmmoUI;
        playerStats.OnTankLeveledUp += HandleTankLevelUp;
        playerStats.OnGunLeveledUp += HandleGunLevelUp;

        UpdateAmmoUI(playerStats.GetCurrentAmmoCount(), playerStats.GetMaxAmmoCount());
        InitializeTankAttribute();
    }
    
    public void UpdateRespawnBar(float percentage)
    {
        respawnBar.fillAmount = percentage;
    }

    public void ResetRespawnBar()
    {
        respawnBar.fillAmount = 0;
    }

    private void InitializeTankAttribute()
    {
        tankNameText.text = playerStats.GetTankName();
        gunNameText.text = playerStats.GetGunName();

        tankImage.sprite = playerStats.GetTankSprite();
        gunImage.sprite = playerStats.GetGunSprite();

        HandleTankLevelUp(playerStats.GetTankLevel());
        HandleGunLevelUp(playerStats.GetGunLevel());
    }

    private void HandleTankLevelUp(int level)
    {
        tankLevelText.text = $"LEVEL {level}";
        tankUpgradeLevelText.text = $"LEVEL {playerStats.GetTankLevel() + 1}";
        tankCostText.text = playerStats.GetTankUpgradeCost().ToString();
    }

    private void HandleGunLevelUp(int level)
    {
        gunLevelText.text = $"LEVEL {level}";
        gunUpgradeLevelText.text = $"LEVEL {playerStats.GetGunLevel() + 1}";
        gunCostText.text = playerStats.GetGunUpgradeCost().ToString();
    }

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoUI.text = $"{currentAmmo} / {maxAmmo}";
    }

    public void Reload(float reloadTime)
    {
        reloadBar.SetReloadTimer(reloadTime);
    }
    public void OpenBuyMenu()
    {
        addOnDisplay.SetActive(false);
        bulletDisplay.SetActive(false);
        upgradePanel.SetActive(true);
    }

    public void CloseBuyMenu()
    {
        addOnDisplay.SetActive(true);
        bulletDisplay.SetActive(true);
        upgradePanel.SetActive(false);
    }

    private void OnDestroy()
    {
        playerStats.OnAmmoUpdated -= UpdateAmmoUI;
        playerStats.OnTankLeveledUp -= HandleTankLevelUp;
        playerStats.OnGunLeveledUp -= HandleGunLevelUp;
    }
}
