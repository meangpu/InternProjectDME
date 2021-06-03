using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankChildSetup : MonoBehaviour
{
    public Image myImageComponent;
    public ObjPlayerTank selfTankData;
    public ChooseTank chooseTankScript;
    
    [SerializeField] Toggle selfToggle;

    private void Start() 
    {
        selfToggle.group = gameObject.transform.parent.GetComponent<ToggleGroup>();
    }

    public void showData(ObjPlayerTank dataTank)
    {
        myImageComponent.sprite = dataTank.GetSprite();
        selfTankData = dataTank;
        chooseTankScript = transform.parent.GetComponent<ChooseTank>();
    }

    public void showTankName()
    {
        chooseTankScript.updateTankData(selfTankData);
    }

    public void displayTank()
    {
        chooseTankScript.firstDisplayTankData(selfTankData);
    }

    public void showOnExit()
    {
        chooseTankScript.showNowTankData();
    }

}
