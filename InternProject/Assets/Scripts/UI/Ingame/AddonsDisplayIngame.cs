using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddonsDisplayIngame : MonoBehaviour
{
    [SerializeField] private PlayerEquippedAddons equippedAddons = null;
    [SerializeField] private Image addonQ = null;
    [SerializeField] private Image addonE = null;
    [SerializeField] private Image addonMagnet = null;
    [SerializeField] private TMP_Text energyCostQText = null;
    [SerializeField] private TMP_Text energyCostEText = null;
    [SerializeField] private TMP_Text energyCostMagnetText = null;
    [SerializeField] private Image rechargeBarQ = null;
    [SerializeField] private Image rechargeBarE = null;
    [SerializeField] private Image rechargeBarMagnet = null;
    [SerializeField] private TMP_Text cooldownDurationQText = null;
    [SerializeField] private TMP_Text cooldownDurationEText = null;
    [SerializeField] private TMP_Text cooldownDurationMagnetText = null;
    [SerializeField] private CooldownSystem cooldownSystem = null;

    private List<ObjAbility> addonsList;

    private ObjAbility objQ;
    private ObjAbility objE;
    private ObjAbility objMagnet;

    private float remainingCooldownQ = 0;
    private float remainingCooldownE = 0;
    private float remainingCooldownMagnet = 0;

    private float remainingPercentageQ = 0;
    private float remainingPercentageE = 0;
    private float remainingPercentageMagnet = 0;

    private void Awake()
    {
        addonsList = equippedAddons.GetEquippedAddons();

        objQ = addonsList[0];
        objE = addonsList[1];
        objMagnet = equippedAddons.GetMagnet();
    }

    private void Start()
    {
        addonQ.sprite = objQ.GetIcon();
        addonE.sprite = objE.GetIcon();
        addonMagnet.sprite = objMagnet.GetIcon();

        energyCostQText.text = objQ.GetEnergyCost().ToString();
        energyCostEText.text = objE.GetEnergyCost().ToString();
        energyCostMagnetText.text = objMagnet.GetEnergyCost().ToString();

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
        if (cooldownSystem.IsOnCooldown(objQ.GetAbilityType()))
        {
            rechargeBarQ.fillAmount = remainingPercentageQ;
            cooldownDurationQText.text = ((int)remainingCooldownQ).ToString();

            if (cooldownDurationQText.gameObject.activeSelf) { goto CheckE; }

            cooldownDurationQText.gameObject.SetActive(true);
            rechargeBarQ.enabled = true;
        } 
        else
        {
            if (!cooldownDurationQText.gameObject.activeSelf) { goto CheckE; }

            cooldownDurationQText.gameObject.SetActive(false);
            rechargeBarQ.enabled = false;
        }

        CheckE:
        if (cooldownSystem.IsOnCooldown(objE.GetAbilityType()))
        {
            rechargeBarE.fillAmount = remainingPercentageE;
            cooldownDurationEText.text = ((int)remainingCooldownE).ToString();

            if (cooldownDurationEText.gameObject.activeSelf) { goto CheckMagnet; }

            cooldownDurationEText.gameObject.SetActive(true);
            rechargeBarE.enabled = true;
        }
        else
        {
            if (!cooldownDurationEText.gameObject.activeSelf) { goto CheckMagnet; }

            cooldownDurationEText.gameObject.SetActive(false);
            rechargeBarE.enabled = false;
        }

        CheckMagnet:
        if (cooldownSystem.IsOnCooldown(objMagnet.GetAbilityType()))
        {
            rechargeBarMagnet.fillAmount = remainingPercentageMagnet;
            cooldownDurationMagnetText.text = ((int)remainingCooldownMagnet).ToString();

            if (cooldownDurationMagnetText.gameObject.activeSelf) { return; }

            cooldownDurationMagnetText.gameObject.SetActive(true);
            rechargeBarMagnet.enabled = true;
        }
        else
        {
            if (!cooldownDurationMagnetText.gameObject.activeSelf) { return; }

            cooldownDurationMagnetText.gameObject.SetActive(false);
            rechargeBarMagnet.enabled = false;
        }
    }

    private void GetRemainingDuration()
    {
        remainingCooldownQ = cooldownSystem.GetRemainingDuration(objQ.GetAbilityType());
        remainingCooldownE = cooldownSystem.GetRemainingDuration(objE.GetAbilityType());
        remainingCooldownMagnet = cooldownSystem.GetRemainingDuration(objMagnet.GetAbilityType());
    }

    private void CalculatePercentage()
    {
        remainingPercentageQ = remainingCooldownQ / objQ.GetCooldown();
        remainingPercentageE = remainingCooldownE / objE.GetCooldown();
        remainingPercentageMagnet = remainingCooldownMagnet / objMagnet.GetCooldown();
    }

    private void ResetCooldownUI()
    {
        rechargeBarQ.fillAmount = remainingPercentageQ;
        rechargeBarE.fillAmount = remainingPercentageE;
        rechargeBarMagnet.fillAmount = remainingPercentageMagnet;

        cooldownDurationQText.gameObject.SetActive(false);
        cooldownDurationEText.gameObject.SetActive(false);
        cooldownDurationMagnetText.gameObject.SetActive(false);

        rechargeBarQ.enabled = false;
        rechargeBarE.enabled = false;
        rechargeBarMagnet.enabled = false;
    }
}
