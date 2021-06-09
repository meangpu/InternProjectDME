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
        UpdateTankAttribute();
    }
    
    public void UpdateRespawnBar(float percentage)
    {
        respawnBar.fillAmount = percentage;
    }

    public void ResetRespawnBar()
    {
        respawnBar.fillAmount = 0;
    }

    private void UpdateTankAttribute()
    {
        tankNameText.text = playerStats.GetTankName();
        gunNameText.text = playerStats.GetGunName();
    }

    private void HandleTankLevelUp(int level)
    {
        tankLevelText.text = $"LEVEL {level}";
    }

    private void HandleGunLevelUp(int level)
    {
        gunLevelText.text = $"LEVEL {level}";
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
    }

    public void CloseBuyMenu()
    {
        addOnDisplay.SetActive(true);
        bulletDisplay.SetActive(true);
    }

    private void OnDestroy()
    {
        playerStats.OnAmmoUpdated -= UpdateAmmoUI;
        playerStats.OnTankLeveledUp -= HandleTankLevelUp;
        playerStats.OnGunLeveledUp -= HandleGunLevelUp;
    }
}
