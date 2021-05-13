using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoUI = null;

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoUI.text = $"{currentAmmo} / {maxAmmo}";
    }
}
