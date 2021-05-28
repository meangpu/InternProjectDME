using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddonsModuleSetup : MonoBehaviour
{
    [SerializeField] private Image image;

    // private ObjAbility addonObject;

    public void DisplayData(ObjAbility addon)
    {
        image.sprite = addon.GetIcon();
    }
}
