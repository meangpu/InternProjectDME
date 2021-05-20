using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text ammoUI = null;
    [SerializeField] private ReloadBar reloadBar = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoUI.text = $"{currentAmmo} / {maxAmmo}";
    }

    public void Reload(float reloadTime)
    {
        reloadBar.SetReloadTimer(reloadTime);
    }
}
