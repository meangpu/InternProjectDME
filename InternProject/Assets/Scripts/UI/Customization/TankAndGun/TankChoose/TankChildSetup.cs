using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankChildSetup : MonoBehaviour
{
    public Image myImageComponent;
    public ObjPlayerTank selfTankData;
    public ChooseTank chooseTankScript;

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

    public void showData(ObjPlayerTank dataTank)
    {
        myImageComponent.sprite = dataTank.GetSprite();
        selfTankData = dataTank;
        chooseTankScript = transform.parent.GetComponent<ChooseTank>();

        if (dataTank.GetIsUnlock())
        {
            // if tank is unlocked disable tank panel
            LockPanel.SetActive(false);
        }
        else
        {
            // if tank is not unlock disable toggle component
            selfToggle.interactable = false;  // player cannot select this tank

            unlockPriceText.text = dataTank.GetBuyStarPrice().ToString();
            if (starManager.Instance.getNowStar() >= dataTank.GetBuyStarPrice())
            {
                // player cannot click to open buy panel if they don't have enough star
                lockButton.interactable = true;
                glowCanbuy.SetActive(true);
            }
            else
            {
                lockButton.interactable = false;
                glowCanbuy.SetActive(false);
            }
        }


        if (selfTankData == chooseTankScript.nowTankGun.nowTankGun.GetTank())
        {
            selfToggle.isOn = true;
        }
    }

    public void unlockThisTank()
    {
        LockPanel.SetActive(false);
        selfToggle.interactable = true;
        unlockPar.Play();
    }

    public void showTankName()
    {
        chooseTankScript.updateTankData(selfTankData);
    }

    public void displayTank()
    {
        chooseTankScript.firstDisplayTankData(selfTankData);
    }

    public void showOnExit()
    {
        chooseTankScript.showNowTankData();
    }


    public void showAskBuy()
    {
        starManager.Instance.showAskTankPanel(selfTankData.GetBuyStarPrice(), selfTankData, this);
    }

}
