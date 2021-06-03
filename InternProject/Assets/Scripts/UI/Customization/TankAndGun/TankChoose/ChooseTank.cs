using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseTank : MonoBehaviour
{
    public Image BigImageOfTank;
    public TMP_Text tankName;
    public TMP_Text tankDes;

    public Slider hp;
    public TMP_Text hpText;

    public Slider hpRegen;
    public TMP_Text hpRegenText;

    public Slider speed;
    public TMP_Text speedText;

    public Slider rotateSpeed;
    public TMP_Text rotateSpeedText;

    public Slider energy;
    public TMP_Text energyText;

    public Slider energyRegen;
    public TMP_Text energyRegenText;

    public NowTankGun nowTankGun;
    
    public void updateTankData(ObjPlayerTank dataTank)
    {
        firstDisplayTankData(dataTank);
        nowTankGun.saveTankData(dataTank);
    }

    public void firstDisplayTankData(ObjPlayerTank dataTank)
    {
        BigImageOfTank.sprite = dataTank.GetSprite();
        tankName.text = dataTank.GetName();
        tankDes.text = dataTank.GetDescription();

        hp.value = dataTank.GetHealth()[1];
        hpText.text = dataTank.GetHealth()[1].ToString();

        hpRegen.value = dataTank.GetHealthRegenRate()[1];
        hpRegenText.text = dataTank.GetHealthRegenRate()[1].ToString();

        speed.value = dataTank.GetMovementSpeed()[1];
        speedText.text = dataTank.GetMovementSpeed()[1].ToString();

        rotateSpeed.value = dataTank.GetRotationSpeed()[1];
        rotateSpeedText.text = dataTank.GetRotationSpeed()[1].ToString();

        energy.value = dataTank.GetEnergy()[1];
        energyText.text = dataTank.GetEnergy()[1].ToString();

        energyRegen.value = dataTank.GetEnergyRegenRate()[1];
        energyRegenText.text = dataTank.GetEnergyRegenRate()[1].ToString();
    }

    public void showNowTankData()
    {
        nowTankGun.updateImageTankGun();
    }
}
