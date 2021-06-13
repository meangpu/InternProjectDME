using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeGunStats : MonoBehaviour
{
    [SerializeField] private TMP_Text[] statsText = null;

    [Header("Colors")]
    [SerializeField] private Color noUpgradeColor;
    [SerializeField] private Color upgradableColor;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        playerStats.OnGunLeveledUp += SetDisplay;
        SetDisplay(0, 0);
    }

    private void SetDisplay(int _1, int _2)
    {
        playerStats.GetNextLevelGunStats(out List<(bool, string)> statsDisplay);

        for (int i = 0; i < statsText.Length; i++)
        {
            statsText[i].text = statsDisplay[i].Item2;

            statsText[i].color = statsDisplay[i].Item1 ? upgradableColor : noUpgradeColor;
        }
    }

    private void OnDestroy()
    {
        playerStats.OnGunLeveledUp -= SetDisplay;
    }
}
