using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform buyCamera = null;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float screenBorderThickness = 10f;
    [SerializeField] private Vector2 screenXLimits = Vector2.zero;
    [SerializeField] private Vector2 screenYLimits = Vector2.zero;

    private Vector2 previousInput;
    private PlayerControls playerControls;
    private bool isInBuyMode = false;

    private void Start()
    {
        playerControls = GameManager.Instance.GetPlayerControls();

        GameManager.Instance.OnBuyModeTrigger += HandleBuyModeTrigger;

        playerControls.BuyMenu.Camera.performed += SetPreviousInput;
        playerControls.BuyMenu.Camera.canceled += SetPreviousInput;

        DisableCameraController();
    }

    private void Update()
    {
        if (!Application.isFocused) { return; }

        if (!isInBuyMode) { return; }

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector2 pos = buyCamera.position;

        if (previousInput == Vector2.zero)
        {
            Vector2 cursorMovement = Vector2.zero;

            Vector2 cursorPosition = Mouse.current.position.ReadValue();

            if (cursorPosition.y >= Screen.height - screenBorderThickness)
            {
                cursorMovement.y += 1;
            }
            else if (cursorPosition.y <= screenBorderThickness)
            {
                cursorMovement.y -= 1;
            }

            if (cursorPosition.x >= Screen.width - screenBorderThickness)
            {
                cursorMovement.x += 1;
            }
            else if (cursorPosition.x <= screenBorderThickness)
            {
                cursorMovement.x -= 1;
            }

            pos += speed * Time.deltaTime * cursorMovement.normalized;
        }
        else
        {
            pos += speed * Time.deltaTime * new Vector2(previousInput.x, previousInput.y);
        }

        pos.x = Mathf.Clamp(pos.x, screenXLimits.x + screenBorderThickness, screenXLimits.y - screenBorderThickness);
        pos.y = Mathf.Clamp(pos.y, screenYLimits.x + screenBorderThickness, screenYLimits.y - screenBorderThickness);

        buyCamera.position = pos;
    }

    public void DisableCameraController()
    {
        playerControls.BuyMenu.Camera.Disable();
    }

    public void EnableCameraController()
    {
        playerControls.BuyMenu.Camera.Enable();
    }

    private void SetPreviousInput(InputAction.CallbackContext ctx)
    {
        previousInput = ctx.ReadValue<Vector2>();
    }

    private void HandleBuyModeTrigger(bool state)
    {
        switch (state)
        {
            case true:
                DisableCameraController();
                isInBuyMode = false;
                return;
            case false:
                EnableCameraController();
                isInBuyMode = true;
                return;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnBuyModeTrigger -= HandleBuyModeTrigger;
    }
}
