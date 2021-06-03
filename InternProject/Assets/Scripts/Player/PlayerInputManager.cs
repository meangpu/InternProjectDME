using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerGun gun;
    private PlayerControls playerControls;
    private PlayerAbilities playerAbilities;

    private void Awake()
    {
        playerControls = new PlayerControls();

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
        playerControls.BuyMenu.BuyMode.performed += _ => GameManager.Instance.BuyModeSwap();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public float GetMoveValue() => playerControls.Tank.Move.ReadValue<float>();
    public float GetRotationValue() => playerControls.Tank.Rotate.ReadValue<float>();
    public Vector2 GetMousePosition() => playerControls.Tank.LookAt.ReadValue<Vector2>();

    // public Vector2 GetMousePositionInBuyMode() => playerControls.BuyMenu.MousePosition.ReadValue<Vector2>();

    public void DisableTankControls()
    {
        playerControls.Tank.Disable();
    }

    public void EnableTankControls()
    {
        playerControls.Tank.Enable();
    }

}
