using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankChildSetup : MonoBehaviour
{
    public Image myImageComponent;
    public Tank selfTankData;
    public ChooseTank chooseTankScript;

    public void showData(Tank dataTank)
    {
        myImageComponent.sprite = dataTank.GetSprite();
        selfTankData = dataTank;
        chooseTankScript = transform.parent.GetComponent<ChooseTank>();
    }

    public void showTankName()
    {
        chooseTankScript.updateTankData(selfTankData);
    }
}