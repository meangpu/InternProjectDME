using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAtPoint : MonoBehaviour
{
    [SerializeField] private Transform gun = null; // Gun Pivoting point
    
    private Camera mainCamera;
    private PlayerInputManager input;

    private Vector2 mousePos;

    private void Start()
    {
        mainCamera = Camera.main;
        input = GetComponentInParent<PlayerInputManager>();
    }

    private void Update()
    {
        mousePos = input.GetMousePosition();
        RotateBarrel();
    }

    private void RotateBarrel()
    {
        Vector2 cursorPosOnScreen = mainCamera.ScreenToWorldPoint(mousePos);

        Vector3 gunDirection = (Vector3)cursorPosOnScreen - gun.position;

        float aimAtAngle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;

        gun.rotation = Quaternion.RotateTowards(gun.rotation, Quaternion.Euler(0, 0, aimAtAngle + 90), Mathf.Infinity);


    }
}
