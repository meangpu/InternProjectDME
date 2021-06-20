using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NowTankGun : MonoBehaviour
{
    public PlayerTankCustomization nowTankGun;
    [SerializeField] Image TankGunImage;
    [SerializeField] Image TankImage;
    [SerializeField] ChooseGun gunScpt;
    [SerializeField] ChooseTank tankScpt;

    private void Start() 
    {
        updateImageTankGun();
    }

    public void updateImageTankGun()
    {
        gunScpt.FirstDisplayGunData(nowTankGun.GetTurret());
        tankScpt.firstDisplayTankData(nowTankGun.GetTank());
    }

    public void saveTankData(ObjPlayerTank _tank)
    {
        nowTankGun.SetTank(_tank);
    }

    public void saveGunData(ObjTankTurret _gun)
    {
        nowTankGun.SetTurret(_gun);
    }


}
