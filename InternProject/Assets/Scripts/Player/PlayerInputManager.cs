using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerGun gun;
    private PlayerControls playerControls;
    private PlayerAbilities playerAbilities;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        playerControls = gameManager.GetPlayerControls();

        gun = GetComponentInChildren<PlayerGun>();
        playerAbilities = GetComponent<PlayerAbilities>();
    }

    private void Start()
    {
        playerControls.Tank.Shoot.performed += _ => gun.OnHoldShootButton();
        playerControls.Tank.Shoot.canceled += _ => gun.OnReleaseShootButton();
        playerControls.Tank.SpecialShoot.performed += _ => gun.ShootSpecial();
        playerControls.Tank.Reload.performed += _ => StartCoroutine(gun.Reload());
        playerControls.Tank.Skill1.performed += _ => playerAbilities.Skill1Activate();
        playerControls.Tank.Skill2.performed += _ => playerAbilities.Skill2Activate();
        playerControls.BuyMenu.BuyMode.performed += _ => gameManager.BuyModeSwap();

        gameManager.OnBuyModeTrigger += BuyModeHandler;
    }

    private void OnDisable()
    {
        DisableTankControls();
        playerControls.BuyMenu.BuyMode.Disable();
    }

    private void OnEnable()
    {
        EnableTankControls();
        playerControls.BuyMenu.BuyMode.Enable();
    }

    public float GetMoveValue() => playerControls.Tank.Move.ReadValue<float>();
    public float GetRotationValue() => playerControls.Tank.Rotate.ReadValue<float>();
    public Vector2 GetMousePosition() => playerControls.Tank.LookAt.ReadValue<Vector2>();

    private void DisableTankControls()
    {
        playerControls.Tank.Disable();
    }

    private void EnableTankControls()
    {
        playerControls.Tank.Enable();
    }

    public void BuyModeHandler(bool state)
    {
        switch (state)
        {
            case true:
                EnableTankControls();
                return;
            case false:
                DisableTankControls();
                return;
        }
    }

    private void OnDestroy()
    {
        gameManager.OnBuyModeTrigger -= BuyModeHandler;
    }
}
