using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Tank tank = null;
    [SerializeField] private Rigidbody2D rb = null;

    private PlayerInputManager input;

    // Player Controls vars
    private float moveDirection;
    private float rotateDirection;

    private float movementSpeed;
    private float rotationSpeed;

    private void Awake()
    {
        input = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        movementSpeed = tank.GetMovementSpeed();
        rotationSpeed = tank.GetRotationSpeed();
    }

    private void Update()
    {
        ReadInputValues();
    }

    private void ReadInputValues() // Read all input values from the Input System
    {
        moveDirection = input.GetMoveValue();
        rotateDirection = input.GetRotationValue();
    }

    public void Move()
    {
        rb.velocity = new Vector2(transform.up.x, transform.up.y) * -moveDirection * movementSpeed;
    }

    public void RotateTank()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -rotateDirection * rotationSpeed));
    }
}
