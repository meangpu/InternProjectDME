using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddonsSelectionInputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Addons.AssignQ.started += _ => Debug.Log("Q");
        playerControls.Addons.AssignE.started += _ => Debug.Log("E");
        playerControls.Addons.Cancel.started += _ => Debug.Log("HOLDUP");
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
