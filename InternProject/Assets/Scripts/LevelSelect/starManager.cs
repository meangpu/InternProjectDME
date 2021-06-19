using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class starManager : MonoBehaviour
{
    [SerializeField] TMP_Text textStarValue;
    [SerializeField] ObjStarData starData;
    [Header("confirmBuyPanel")]
    [SerializeField] GameObject confirmPanel;
    [SerializeField] TMP_Text confirmBuyText;

    [Header("parent list script")]
    [SerializeField] ListOfTank lostTankScpt;
    [SerializeField] ListOfGun listGunScpt;
    [SerializeField] ListOfAddons listAddOnScpt;

    ObjPlayerTank goingToBuyTank;
    TankChildSetup goingToBuyTankScpt;

    ObjTankTurret goingToBuyGun;
    GunChildSetup goingToBuyGunScpt;

    ObjAbility goingToBuyAddon;
    AddonsModuleSetup goingToBuyAddonScpt;


    public static starManager Instance { get { return _instance; } }
    private static starManager _instance; 

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;  //Avoid doing anything else
        }
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start() 
    {
        textStarValue.text = starData.GetStar().ToString();
    }

    public int getNowStar()
    {
        return starData.GetStar();
    }

    public void showAskTankPanel(int _price, ObjPlayerTank _tankObj, TankChildSetup _childScpt)
    {
        goingToBuyTank = _tankObj;
        goingToBuyTankScpt = _childScpt;
        confirmPanel.SetActive(true);
        confirmBuyText.text = $"Use <size=80><color=yellow>{_price}</color></size> stars\n<size=25> to unlock this tank?</size>";
    }

    public void showAskGunPanel(int _price, ObjTankTurret _gunObj, GunChildSetup _childScpt)
    {
        goingToBuyGun = _gunObj;
        goingToBuyGunScpt = _childScpt;
        confirmPanel.SetActive(true);
        confirmBuyText.text = $"Use <size=80><color=yellow>{_price}</color></size> stars\n<size=25> to unlock this gun?</size>";
    }

    public void showAskAddonPanel(int _price, ObjAbility _addonObj, AddonsModuleSetup _childScpt)
    {
        goingToBuyAddon = _addonObj;
        goingToBuyAddonScpt = _childScpt;
        confirmPanel.SetActive(true);
        confirmBuyText.text = $"Use <size=80><color=yellow>{_price}</color></size> stars\n<size=25> to unlock this addon?</size>";
    }


    public void buy()
    {
        if (goingToBuyTank != null)
        {
            buyTank();
        }
        else if (goingToBuyGun != null)
        {
            buyGun();
        }
        else if (goingToBuyAddon != null)
        {
            buyAddon();
        }
        else
        {
            Debug.Log("How tf is this error happen");
        }
        lostTankScpt.resetAfterBuy();
        listGunScpt.resetAfterBuy();
        listAddOnScpt.resetAfterBuy();

    }


    public void updateAfterBuy()
    {
        confirmPanel.SetActive(false);
        textStarValue.text = starData.GetStar().ToString();
        goingToBuyTank = null;
        goingToBuyTankScpt = null;
        goingToBuyGun = null;
        goingToBuyGunScpt = null;
        goingToBuyAddon = null;
        goingToBuyAddonScpt = null;
    }

    public void buyTank()
    {
        if (starData.GetStar() >= goingToBuyTank.GetBuyStarPrice())
        {
            goingToBuyTank.unlockThisTank();  // in obj
            starData.subtractValue(goingToBuyTank.GetBuyStarPrice());
        }
        goingToBuyTankScpt.unlockThisTank();  // in child script
        updateAfterBuy();
    }

    public void buyGun()
    {
        if (starData.GetStar() >= goingToBuyGun.GetBuyStarPrice())
        {
            goingToBuyGun.unlockThisGun();  // in obj
            starData.subtractValue(goingToBuyGun.GetBuyStarPrice());
        }
        goingToBuyGunScpt.unlockThisGun();  // in child script
        updateAfterBuy();
    }

    public void buyAddon()
    {
        if (starData.GetStar() >= goingToBuyAddon.GetBuyStarPrice())
        {
            goingToBuyAddon.unlockThisAddon();  // in obj
            starData.subtractValue(goingToBuyAddon.GetBuyStarPrice());
        }
        goingToBuyAddonScpt.unlockThisAddon();  // in child script
        updateAfterBuy();
    }



    public void cancelBuy()
    {
        confirmPanel.SetActive(false);

        goingToBuyTank = null;
        goingToBuyTankScpt = null;
        goingToBuyGun = null;
        goingToBuyGunScpt = null;
        goingToBuyAddon = null;
        goingToBuyAddonScpt = null;
    }


}
