using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerChildDisplay : MonoBehaviour
{
    [SerializeField] Image towerimage;
    [SerializeField] TMP_Text priceText;

    public void displayImg(ObjTower _tower)
    {
        towerimage.sprite = _tower.GetTowerSprite();
        priceText.text = _tower.GetUpgradeCost()[0].ToString();
    }

}
