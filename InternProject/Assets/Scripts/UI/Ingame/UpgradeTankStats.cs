using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeTankStats : MonoBehaviour
{
    [SerializeField] private TMP_Text[] statsText = null;

    [Header("Colors")]
    [SerializeField] private Color noUpgradeColor;
    [SerializeField] private Color upgradableColor;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        playerStats.OnTankLeveledUp += SetDisplay;
        SetDisplay(0, 0);
    }

    private void SetDisplay(int _1, int _2)
    {
        playerStats.GetNextLevelTankStats(out List<(bool, string)> statsDisplay);

        for (int i = 0; i < statsText.Length; i++)
        {
            switch (statsDisplay[i].Item1) 
            {
                case true:
                    string[] stats = statsDisplay[i].Item2.Split('>');
                    statsText[i].SetText($"{stats[0].AddColor(noUpgradeColor)}>{stats[1].AddColor(upgradableColor)}");
                    break;

                case false:
                    statsText[i].SetText($"{statsDisplay[i].Item2.AddColor(noUpgradeColor)}");
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        playerStats.OnTankLeveledUp -= SetDisplay;
    }
}

public static class StringExtensions
{
    public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
    public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
}
