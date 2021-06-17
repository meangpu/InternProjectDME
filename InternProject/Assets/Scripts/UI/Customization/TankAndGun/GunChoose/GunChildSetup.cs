using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunChildSetup : MonoBehaviour
{
    public Image myImageComponent;
    public ObjTankTurret selfTurret;
    public ChooseGun chooseGunScript;

    [Header("selfValue")]
    [SerializeField] GameObject LockPanel;
    [SerializeField] Button lockButton;
    [SerializeField] GameObject glowCanbuy;
    [SerializeField] TMP_Text unlockPriceText;
    [SerializeField] ParticleSystem unlockPar;


    [SerializeField] Toggle selfToggle;

    private void Start() 
    {
        selfToggle.group = gameObject.transform.parent.GetComponent<ToggleGroup>();
    }


    public void ShowData(ObjTankTurret gunData)
    {
        myImageComponent.sprite = gunData.GetSprite();
        selfTurret = gunData;
        chooseGunScript = transform.parent.GetComponent<ChooseGun>();


        if (selfTurret.GetIsUnlock())
        {
            // if tank is unlocked disable tank panel
            LockPanel.SetActive(false);
        }
        else
        {
            // if tank is not unlock disable toggle component
            selfToggle.interactable = false;  // player cannot select this tank

            unlockPriceText.text = selfTurret.GetBuyStarPrice().ToString();
            if (starManager.Instance.getNowStar() >= selfTurret.GetBuyStarPrice())
            {
                // player cannot click to open buy panel if they don't have enough star
                lockButton.interactable = true;
                glowCanbuy.SetActive(true);
            }
        }


        if (selfTurret == chooseGunScript.nowTankGun.nowTankGun.GetTurret())
        {
            selfToggle.isOn = true;
        }
    }

    public void unlockThisGun()
    {
        LockPanel.SetActive(false);
        selfToggle.interactable = true;
        unlockPar.Play();
    }


    public void ShowGunName()
    {
        chooseGunScript.updateGunData(selfTurret);
    }

    public void DisplayGun()
    {
        chooseGunScript.firstDisplayGunData(selfTurret);
    }

    public void showOnExit()
    {
        chooseGunScript.showNowGunData();
    }


    public void showAskBuy()
    {
        starManager.Instance.showAskGunPanel(selfTurret.GetBuyStarPrice(), selfTurret, this);
    }


}
