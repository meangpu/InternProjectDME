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
    public Slider speed;
    public Slider energy;
    
    public void updateTankData(ObjPlayerTank dataTank)
    {
        BigImageOfTank.sprite = dataTank.GetSprite();
        tankName.text = dataTank.GetName();
        tankDes.text = dataTank.GetDescription();
        hp.value = dataTank.GetHealth();
        speed.value = dataTank.GetMovementSpeed();
        energy.value = dataTank.GetEnergy();

    }
}
