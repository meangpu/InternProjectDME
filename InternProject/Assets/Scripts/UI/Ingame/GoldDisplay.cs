using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText = null;

    private PlayerStats playerStats;
    private GoldSystem goldSystem;

    private UIManager uiManager;

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        goldSystem = playerStats.GetGoldSystem();

        uiManager = UIManager.Instance;

        UpdateUI(goldSystem.GetGold());

        goldSystem.OnGoldUpdated += HandleGoldUpdated;
        goldSystem.OnNotEnoughGold += HandleNotEnoughGold;
    }

    private void HandleNotEnoughGold()
    {
        uiManager.TriggerNotEnoughGold();
    }

    private void HandleGoldUpdated(int ObjGold)
    {
        UpdateUI(ObjGold);
    }

    private void UpdateUI(int goldRemaining)
    {
        goldText.text = goldRemaining.ToString();
    }

    private void OnDestroy()
    {
        goldSystem.OnGoldUpdated -= HandleGoldUpdated;
        goldSystem.OnNotEnoughGold -= HandleNotEnoughGold;
    }
}
