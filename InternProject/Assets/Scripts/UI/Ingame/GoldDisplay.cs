using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText = null;

    private PlayerStats playerStats;
    private GoldSystem goldSystem;

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        goldSystem = playerStats.GetGoldSystem();

        UpdateUI(goldSystem.GetGold());

        goldSystem.OnGoldUpdated += HandleGoldUpdated;
        goldSystem.OnNotEnoughGold += HandleNotEnoughGold;
    }

    private void HandleNotEnoughGold()
    {
        Debug.Log("NOT ENOUGH ObjGold");
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
