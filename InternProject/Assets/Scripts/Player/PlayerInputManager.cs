using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private Player player;
    private PlayerGun gun;
    private PlayerControls playerControls;
    private PlayerAbilities playerAbilities;

    private void Awake()
    {
        playerControls = new PlayerControls();

        player = GetComponent<Player>();
        gun = GetComponentInChildren<PlayerGun>();
        playerAbilities = GetComponent<PlayerAbilities>();
    }

    private void Start()
    {
        playerControls.Tank.Shoot.performed += _ => gun.OnHoldShootButton();
        playerControls.Tank.Shoot.canceled += _ => gun.OnReleaseShootButton();
        playerControls.Tank.SpecialShoot.performed += _ => player.SpecialShoot();
        playerControls.Tank.Reload.performed += _ => StartCoroutine(gun.Reload());
        playerControls.Tank.Skill1.performed += _ => playerAbilities.Skill1Activate();
        playerControls.Tank.Skill2.performed += _ => playerAbilities.Skill2Activate();
        playerControls.BuyMenu.BuyMode.performed += _ => Debug.Log("BUY MENU PLZ");
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

    public void DisableMovement()
    {
        playerControls.Tank.Move.Disable();
    }

    public void DisableRotation()
    {
        playerControls.Tank.Rotate.Disable();
    }

    public void EnableMovement()
    {
        playerControls.Tank.Move.Enable();
    }

    public void EnableRotation()
    {
        playerControls.Tank.Rotate.Enable();
    }

}
