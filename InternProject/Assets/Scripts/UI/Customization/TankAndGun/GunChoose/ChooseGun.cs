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

    [Header("slider")]
    public Slider dmg;
    public TMP_Text dmgT;

    public Slider ammo;
    public TMP_Text ammoT;

    public Slider fireRate;
    public TMP_Text fireRateT;

    public Slider reload;
    public TMP_Text reloadT;

    public Slider speed;
    public TMP_Text speedT;

    public Slider knockback;
    public TMP_Text knockbackT;

    [Space]
    public NowTankGun nowTankGun;
    
    public void UpdateGunData(ObjTankTurret dataGun)
    {
        FirstDisplayGunData(dataGun);
        nowTankGun.saveGunData(dataGun);
    }

    public void FirstDisplayGunData(ObjTankTurret dataGun)
    {
        TankGun.sprite = dataGun.GetSprite();
        gunName.text = dataGun.GetName();
        gunDes.text = dataGun.GetDescription();

        dmg.value = dataGun.GetMaxDamage()[1];
        dmgT.text = dataGun.GetMaxDamage()[1].ToString();

        ammo.value = dataGun.GetAmmoCount()[1];
        ammoT.text = dataGun.GetAmmoCount()[1].ToString();

        fireRate.value = dataGun.GetRateOfFire()[1];
        fireRateT.text = dataGun.GetRateOfFire()[1].ToString();

        reload.value = dataGun.GetReloadTime()[1];
        reloadT.text = dataGun.GetReloadTime()[1].ToString();

        speed.value = dataGun.GetBulletSpeed()[1];
        speedT.text = dataGun.GetBulletSpeed()[1].ToString();

        knockback.value = dataGun.GetKnockBack();
        knockbackT.text = dataGun.GetKnockBack().ToString();

        reload.value = dataGun.GetReloadTime()[1];
    }

    public void ShowNowGunData()
    {
        nowTankGun.updateImageTankGun();
    }
}
