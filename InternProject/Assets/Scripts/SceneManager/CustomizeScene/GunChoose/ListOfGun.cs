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
            newTurretButton.GetComponent<GunChildSetup>().showData(gun); 
            
        }

    }

}
