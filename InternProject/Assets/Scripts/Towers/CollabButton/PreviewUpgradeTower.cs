using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreviewUpgradeTower : MonoBehaviour
{
    [SerializeField] GameObject previewPanel;
	[SerializeField] TMP_Text priceText;
    [SerializeField] Transform towerParent;
	[Header("TowerInfo")]
	[SerializeField] TMP_Text upgradeName;
	[SerializeField] TMP_Text textDmg;
	[SerializeField] TMP_Text textRange;
	[SerializeField] TMP_Text textAtkSpeed;
	[SerializeField] Slider sliderDmg;
	[SerializeField] Slider sliderRange;
	[SerializeField] Slider sliderAtkSpeed;
    [SerializeField] int maxDmg;
    [SerializeField] float maxRange;
    [SerializeField] int AtkSpeed;

    public void previewUpgrade()
    {
        TowerStats _stat = towerParent.GetChild(0).GetComponent<TowerStats>();
        priceText.text = _stat.GetPrice().ToString();

        upgradeName.text = $"level{_stat.GetTowerLevel().ToString()}";

        textDmg.text = _stat.NextLVDmg().ToString();
        textRange.text = _stat.NextLVRange().ToString();
        textAtkSpeed.text = _stat.GetRateOfFire().ToString();

        sliderDmg.value = (_stat.NextLVDmg()/maxDmg);
        sliderRange.value = (_stat.NextLVRange()/maxRange);
        sliderAtkSpeed.value = (_stat.GetRateOfFire()/AtkSpeed);


        previewPanel.SetActive(true);
    }

    public void closePreviewUpgrade()
    {
        previewPanel.SetActive(false);
    }
}