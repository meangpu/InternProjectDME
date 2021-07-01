using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfTank : MonoBehaviour
{
    [SerializeField] ObjPlayerTank[] TankLists;
    [SerializeField] GameObject TankImgPrefab;


    private void Start() 
    {
        foreach (var ObjPlayerTank in TankLists)
        {
            GameObject newTankButton = Instantiate(TankImgPrefab, gameObject.transform);
            newTankButton.GetComponent<TankChildSetup>().showData(ObjPlayerTank); 
        }
    }


    public void resetAfterBuy()
    {
        // inorder to update unlock icon in case that it cannot buy anymore
        int i = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<TankChildSetup>().showData(TankLists[i]);
            i++;
        }
    }

}
