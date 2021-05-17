using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfTank : MonoBehaviour
{
    [SerializeField] Tank[] TankLists;
    [SerializeField] GameObject TankImgPrefab;


    private void Start() 
    {
        foreach (var tank in TankLists)
        {
            GameObject newTankButton = Instantiate(TankImgPrefab, gameObject.transform);
            newTankButton.GetComponent<TankChildSetup>().showData(tank); 
            
        }

    }
}
