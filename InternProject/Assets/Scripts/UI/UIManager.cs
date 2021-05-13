using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoUI = null;

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoUI.text = $"{currentAmmo} / {maxAmmo}";
    }
}
