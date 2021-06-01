using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddonsDisplayIngame : MonoBehaviour
{
    [SerializeField] private PlayerEquippedAddons equippedAddons = null;
    [SerializeField] private Image addonQ = null;
    [SerializeField] private Image addonE = null;
    [SerializeField] private TMP_Text energyCostQText = null;
    [SerializeField] private TMP_Text energyCostEText = null;
    [SerializeField] private Image rechargeBarQ = null;
    [SerializeField] private Image rechargeBarE = null;
    [SerializeField] private TMP_Text cooldownDurationQText = null;
    [SerializeField] private TMP_Text cooldownDurationEText = null;
    [SerializeField] private CooldownSystem cooldownSystem = null;

    private List<ObjAbility> addonsList;

    private ObjAbility objQ;
    private ObjAbility objE;

    private float remainingCooldownQ = 0;
    private float remainingCooldownE = 0;

    private float remainingPercentageQ = 0;
    private float remainingPercentageE = 0;

    private void Awake()
    {
        addonsList = equippedAddons.GetEquippedAddons();

        objQ = addonsList[0];
        objE = addonsList[1];
    }

    private void Start()
    {
        addonQ.sprite = objQ.GetIcon();
        addonE.sprite = objE.GetIcon();

        energyCostQText.text = objQ.GetEnergyCost().ToString();
        energyCostEText.text = objE.GetEnergyCost().ToString();

        ResetCooldownUI();
    }

    private void Update()
    {
        ProcessCooldownUIs();

        GetRemainingDuration();
        CalculatePercentage();
    }

    private void ProcessCooldownUIs()
    {
        if (remainingPercentageQ != 0)
        {
            rechargeBarQ.fillAmount = remainingPercentageQ;
            cooldownDurationQText.text = ((int)remainingCooldownQ).ToString();

            if (cooldownDurationQText.gameObject.activeInHierarchy) { return; }

            cooldownDurationQText.gameObject.SetActive(true);
        } 
        else
        {
            if (!cooldownDurationQText.gameObject.activeInHierarchy) { return; }

            cooldownDurationQText.gameObject.SetActive(false);
        }

        if (remainingPercentageE != 0)
        {
            rechargeBarE.fillAmount = remainingPercentageE;
            cooldownDurationEText.text = ((int)remainingCooldownE).ToString();

            if (cooldownDurationEText.gameObject.activeInHierarchy) { return; }

            cooldownDurationEText.gameObject.SetActive(true);
        }
        else
        {
            if (!cooldownDurationEText.gameObject.activeInHierarchy) { return; }

            cooldownDurationEText.gameObject.SetActive(false);
        }
    }


    private void GetRemainingDuration()
    {
        remainingCooldownQ = cooldownSystem.GetRemainingDuration(objQ.GetAbilityType());
        remainingCooldownE = cooldownSystem.GetRemainingDuration(objE.GetAbilityType());
    }

    private void CalculatePercentage()
    {
        remainingPercentageQ = remainingCooldownQ / objQ.GetCooldown();
        remainingPercentageE = remainingCooldownE / objE.GetCooldown();
    }

    private void ResetCooldownUI()
    {
        rechargeBarQ.fillAmount = remainingPercentageQ;
        rechargeBarE.fillAmount = remainingPercentageE;

        cooldownDurationQText.gameObject.SetActive(false);
        cooldownDurationEText.gameObject.SetActive(false);
    }
}
