using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankChildSetup : MonoBehaviour
{
    public Image myImageComponent;
    public ObjPlayerTank selfTankData;
    public ChooseTank chooseTankScript;
    public GameObject LockPanel;
    
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

        if (dataTank.GetIsUnlock())
        {
            // if tank is unlocked disable tank panel
            LockPanel.SetActive(false);
        }
        else
        {
            // if tank is not unlock disable toggle component
            selfToggle.interactable = false;
        }


        if (selfTankData == chooseTankScript.nowTankGun.nowTankGun.GetTank())
        {
            selfToggle.isOn = true;
        }
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
