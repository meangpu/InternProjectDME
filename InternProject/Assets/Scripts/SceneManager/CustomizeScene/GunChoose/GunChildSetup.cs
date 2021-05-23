using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunChildSetup : MonoBehaviour
{
    public Image myImageComponent;
    public TankTurret selfTurret;
    public ChooseGun chooseGunScript;

    [SerializeField] Toggle selfToggle;

    private void Start() 
    {
        selfToggle.group = gameObject.transform.parent.GetComponent<ToggleGroup>();
    }


    public void showData(TankTurret gunData)
    {
        myImageComponent.sprite = gunData.GetSprite();
        selfTurret = gunData;
        chooseGunScript = transform.parent.GetComponent<ChooseGun>();
    }

    public void showGunName()
    {
        chooseGunScript.updateGunData(selfTurret);
        TankCustomizationData.playerTurret = selfTurret;
    }

}
