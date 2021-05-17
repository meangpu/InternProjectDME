using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseGun : MonoBehaviour
{
    // public Image TankGun;
    public TMP_Text gunName;
    public TMP_Text gunDes;
    // public Slider hp;
    // public Slider speed;
    // public Slider energy;
    
    public void updateGunData(TankTurret dataGun)
    {
        // TankGun.sprite = dataGun.GetSprite();
        gunName.text = dataGun.GetName();
        gunDes.text = dataGun.GetDescription();
        // hp.value = dataTank.GetHealth();
        // speed.value = dataTank.GetMovementSpeed();
        // energy.value = dataTank.Get();

    }
}
