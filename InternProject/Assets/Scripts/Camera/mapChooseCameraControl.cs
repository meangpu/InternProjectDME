using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class mapChooseCameraControl : MonoBehaviour
{

    [SerializeField] private Transform buyCamera = null;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float screenBorderThickness = 10f;
    [SerializeField] private Vector2 screenXLimits = Vector2.zero;
    [SerializeField] private Vector2 screenYLimits = Vector2.zero;

    private Vector2 previousInput;
    private PlayerControls playerControls;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        playerControls = new PlayerControls();

        playerControls.BuyMenu.Camera.performed += SetPreviousInput;
        playerControls.BuyMenu.Camera.canceled += SetPreviousInput;
        EnableCameraController();

    }

    private void Update()
    {
        if (!Application.isFocused) { return; }

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector2 pos = buyCamera.position;

        Vector2 margin = cam.ScreenToWorldPoint((Vector2.up * Screen.height / 2) + (Vector2.right * Screen.width / 2)) - cam.ScreenToWorldPoint(Vector2.zero);

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

        float marginX = margin.x;
        float marginY = margin.y;

        float camMaxX = screenXLimits.y - marginX;
        float camMaxY = screenYLimits.y - marginY;
        float camMinX = screenXLimits.x + marginX;
        float camMinY = screenYLimits.x + marginY;

        pos.x = Mathf.Clamp(pos.x, camMinX, camMaxX);
        pos.y = Mathf.Clamp(pos.y, camMinY, camMaxY);

        buyCamera.position = pos;
    }

    public void EnableCameraController()
    {
        playerControls.BuyMenu.Camera.Enable();
    }

    private void SetPreviousInput(InputAction.CallbackContext ctx)
    {
        previousInput = ctx.ReadValue<Vector2>();
    }


}
