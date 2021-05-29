using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfAddons : MonoBehaviour
{
    [SerializeField] private ObjAbility[] addonsList;
    [SerializeField] private GameObject addonModulePrefab;

    private void Start()
    {
        foreach (ObjAbility addon in addonsList)
        {
            GameObject newAddonDisplay = Instantiate(addonModulePrefab, gameObject.transform);
            newAddonDisplay.GetComponent<AddonsModuleSetup>().DisplayData(addon);
        }

    }
}