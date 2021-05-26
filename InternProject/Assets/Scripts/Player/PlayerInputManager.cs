using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private Player player;
    private PlayerGun gun;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        player = GetComponent<Player>();
        gun = GetComponentInChildren<PlayerGun>();

        playerControls.ObjPlayerTank.Shoot.performed += _ => gun.OnHoldShootButton();
        playerControls.ObjPlayerTank.Shoot.canceled += _ => gun.OnReleaseShootButton();
        playerControls.ObjPlayerTank.SpecialShoot.performed += _ => player.SpecialShoot();
        playerControls.ObjPlayerTank.Reload.performed += _ => StartCoroutine(gun.Reload());
        playerControls.ObjPlayerTank.Skill1.performed += _ => player.Skill1Activate();
        playerControls.ObjPlayerTank.Skill2.performed += _ => player.Skill2Activate();
        // playerControls.ObjPlayerTank.Pause.performed += _ => player.Pause();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public float GetMoveValue() => playerControls.ObjPlayerTank.Move.ReadValue<float>();
    public float GetRotationValue() => playerControls.ObjPlayerTank.Rotate.ReadValue<float>();
    public Vector2 GetMousePosition() => playerControls.ObjPlayerTank.LookAt.ReadValue<Vector2>();

    public void DisableMovement()
    {
        playerControls.ObjPlayerTank.Move.Disable();
    }

    public void DisableRotation()
    {
        playerControls.ObjPlayerTank.Rotate.Disable();
    }

    public void EnableMovement()
    {
        playerControls.ObjPlayerTank.Move.Enable();
    }

    public void EnableRotation()
    {
        playerControls.ObjPlayerTank.Rotate.Enable();
    }

}
