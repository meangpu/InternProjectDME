using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunChildSetup : MonoBehaviour
{
    public Image myImageComponent;
    public ObjTankTurret selfTurret;
    public ChooseGun chooseGunScript;

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
        if (selfTurret == chooseGunScript.nowTankGun.nowTankGun.GetTurret())
        {
            selfToggle.isOn = true;
        }
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

}
