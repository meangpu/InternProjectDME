using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PreviewSellTower : MonoBehaviour
{
    [SerializeField] GameObject previewPanel;
	[SerializeField] TMP_Text priceText;
    [SerializeField] Transform towerParent;

    public void previewSell()
    {
        TowerStats _stat = towerParent.GetChild(0).GetComponent<TowerStats>();
        priceText.text = _stat.getSellPrice().ToString();
        previewPanel.SetActive(true);
    }

    public void closePreviewSell()
    {
        previewPanel.SetActive(false);
    }


}
