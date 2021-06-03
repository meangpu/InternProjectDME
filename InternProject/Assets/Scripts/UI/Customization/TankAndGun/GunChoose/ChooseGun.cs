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
    [SerializeField] NowTankGun nowTankGun;
    
    public void updateGunData(ObjTankTurret dataGun)
    {
        TankGun.sprite = dataGun.GetSprite();
        gunName.text = dataGun.GetName();
        gunDes.text = dataGun.GetDescription();
        dmg.value = dataGun.GetMaxDamage()[0];
        ammo.value = dataGun.GetAmmoCount()[0];
        rateOfFire.value = dataGun.GetRateOfFire()[0];
        reload.value = dataGun.GetReloadTime()[0];
        nowTankGun.saveGunData(dataGun);
    }

    public void firstDisplayGunData(ObjTankTurret dataGun)
    {
        TankGun.sprite = dataGun.GetSprite();
        gunName.text = dataGun.GetName();
        gunDes.text = dataGun.GetDescription();
        dmg.value = dataGun.GetMaxDamage()[0];
        ammo.value = dataGun.GetAmmoCount()[0];
        rateOfFire.value = dataGun.GetRateOfFire()[0];
        reload.value = dataGun.GetReloadTime()[0];
    }

    public void showNowGunData()
    {
        nowTankGun.updateImageTankGun();
    }
}
