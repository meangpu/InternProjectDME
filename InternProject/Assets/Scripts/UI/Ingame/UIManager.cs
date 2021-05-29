using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text ammoUI = null;
    [SerializeField] private ReloadBar reloadBar = null;
    [SerializeField] private TMP_Text tankNameText = null;
    [SerializeField] private TMP_Text tankLevelText = null;

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

        playerStats = PlayerStats.Instance;
    }

    private void Start()
    {
        playerStats.OnAmmoUpdated += UpdateAmmoUI;
        playerStats.OnTankLeveledUp += HandleTankLevelUp;

        UpdateAmmoUI(playerStats.GetCurrentAmmoCount(), playerStats.GetMaxAmmoCount());
        UpdateTankName();
    }

    private void UpdateTankName()
    {
        tankNameText.text = playerStats.GetTankName();
    }

    private void HandleTankLevelUp(int level)
    {
        tankLevelText.text = $"LEVEL {level}";
    }

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoUI.text = $"{currentAmmo} / {maxAmmo}";
    }

    public void Reload(float reloadTime)
    {
        reloadBar.SetReloadTimer(reloadTime);
    }

    private void OnDestroy()
    {
        playerStats.OnAmmoUpdated -= UpdateAmmoUI;
        playerStats.OnTankLeveledUp -= HandleTankLevelUp;
    }
}