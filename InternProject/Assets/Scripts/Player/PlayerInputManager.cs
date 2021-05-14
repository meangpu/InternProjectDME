using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private Player player;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        player = GetComponent<Player>();

        playerControls.Tank.Shoot.performed += _ => player.OnHoldShootButton();
        playerControls.Tank.Shoot.canceled += _ => player.OnReleaseShootButton();
        playerControls.Tank.SpecialShoot.performed += _ => player.SpecialShoot();
        playerControls.Tank.Reload.performed += _ => StartCoroutine(player.Reload());
        playerControls.Tank.Skill1.performed += _ => player.Skill1Activate();
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
