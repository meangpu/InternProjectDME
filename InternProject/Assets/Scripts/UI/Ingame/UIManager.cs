using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

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
    [SerializeField] private GameObject deathPanel = null;

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

    [Header("Animators")]
    [SerializeField] private Animator ammoAnimator = null;
    [SerializeField] private Animator energyAnimator = null;
    [SerializeField] private Animator goldAnimator = null;

    private PlayerStats playerStats;
    public UnityEvent OnLeveledUp;

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

        SetTankLevelUI(playerStats.GetTankLevel(), playerStats.GetMaxLevel());
        SetGunLevelUI(playerStats.GetGunLevel(), playerStats.GetMaxLevel());
    }

    private void HandleTankLevelUp(int level, int maxLevel)
    {
        SetTankLevelUI(level, maxLevel);
        OnLeveledUp?.Invoke();
    }

    private void SetTankLevelUI(int level, int maxLevel)
    {
        tankLevelText.text = $"LEVEL {level}";

        if (level == maxLevel)
        {
            tankUpgradeLevelText.text = "MAXED";
            tankCostText.text = "-";
        }
        else
        {
            tankUpgradeLevelText.text = $"LEVEL {playerStats.GetTankLevel() + 1}";
            tankCostText.text = playerStats.GetTankUpgradeCost().ToString();
        }
    }

    private void HandleGunLevelUp(int level, int maxLevel)
    {
        SetGunLevelUI(level, maxLevel);
        OnLeveledUp?.Invoke();
    }

    private void SetGunLevelUI(int level, int maxLevel)
    {
        gunLevelText.text = $"LEVEL {level}";

        if (level == maxLevel)
        {
            gunUpgradeLevelText.text = "MAXED";
            gunCostText.text = "-";
        }
        else
        {
            gunUpgradeLevelText.text = $"LEVEL {playerStats.GetGunLevel() + 1}";
            gunCostText.text = playerStats.GetGunUpgradeCost().ToString();
        }
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

    public void HandleDeathUI()
    {
        deathPanel.SetActive(true);
    }

    public void HideDeathPanel()
    {
        deathPanel.SetActive(false);
    }

    public void TriggerNotEnoughAmmo()
    {
        ammoAnimator.SetTrigger("Flash");
    }

    public void TriggerNotEnoughGold()
    {
        goldAnimator.SetTrigger("Flash");
    }

    public void TriggerNotEnoughEnergy()
    {
        energyAnimator.SetTrigger("Flash");
    }

    private void OnDestroy()
    {
        playerStats.OnAmmoUpdated -= UpdateAmmoUI;
        playerStats.OnTankLeveledUp -= HandleTankLevelUp;
        playerStats.OnGunLeveledUp -= HandleGunLevelUp;
    }
}
