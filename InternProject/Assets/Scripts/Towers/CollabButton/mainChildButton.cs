using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainChildButton : MonoBehaviour
{

    [SerializeField] ParentTowerButton parentScript;


    private void OnMouseDown() 
    {
        parentScript.ToggleMenu();
    }





}
