using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfGun : MonoBehaviour
{
    [SerializeField] ObjTankTurret[] TurretList;
    [SerializeField] GameObject TurretPrefabs;

    private void Start() 
    {
        foreach (var gun in TurretList)
        {
            GameObject newTurretButton = Instantiate(TurretPrefabs, gameObject.transform);
            newTurretButton.GetComponent<GunChildSetup>().ShowData(gun); 
            
        }
    }

    public void resetAfterBuy()
    {
        // inorder to update unlock icon in case that it cannot buy anymore
        int i = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<GunChildSetup>().ShowData(TurretList[i]);
            i++;
        }
    }

}
