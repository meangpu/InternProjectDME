using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseGun : MonoBehaviour
{
    public Image TankGun;
    public TMP_Text gunName;
    public TMP_Text gunDes;
    public Slider dmg;
    public Slider ammo;
    public Slider rateOfFire;
    public Slider reload;
    
    public void updateGunData(ObjTankTurret dataGun)
    {
        TankGun.sprite = dataGun.GetSprite();
        gunName.text = dataGun.GetName();
        gunDes.text = dataGun.GetDescription();

        dmg.value = dataGun.GetMaxDamage();
        ammo.value = dataGun.GetAmmoCount();
        rateOfFire.value = dataGun.GetRateOfFire();
        reload.value = dataGun.GetReloadTime();

    }
}
