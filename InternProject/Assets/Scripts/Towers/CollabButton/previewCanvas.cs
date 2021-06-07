using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class previewCanvas : MonoBehaviour
{
	[Header("TowerInfo")]
	[SerializeField] TMP_Text towerName;
	[SerializeField] TMP_Text textDmg;
	[SerializeField] TMP_Text textRange;
	[SerializeField] TMP_Text textAtkSpeed;
	[SerializeField] Slider sliderDmg;
	[SerializeField] Slider sliderRange;
	[SerializeField] Slider sliderAtkSpeed;
    [SerializeField] int maxDmg;
    [SerializeField] float maxRange;
    [SerializeField] int AtkSpeed;

    public void setPreview(ObjTower _tower)
    {
        towerName.text = _tower.GetName();
        textDmg.text = _tower.GetMaxDamage()[0].ToString();
        textRange.text = _tower.GetAttackRange()[0].ToString();
        textAtkSpeed.text = _tower.GetRateOfFire().ToString();

        sliderDmg.value = (_tower.GetMaxDamage()[0]/maxDmg);
        sliderRange.value = (_tower.GetAttackRange()[0]/maxRange);
        sliderAtkSpeed.value = (_tower.GetRateOfFire()/AtkSpeed);
    }

}
