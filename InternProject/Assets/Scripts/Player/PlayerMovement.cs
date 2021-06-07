using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    private PlayerInputManager input;
    private PlayerStats playerStats;

    // Player Controls vars
    private float moveDirection;
    private float rotateDirection;

    private float movementSpeed;
    private float rotationSpeed;

    private void Awake()
    {
        input = GetComponent<PlayerInputManager>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        GetTankAttributes();
    }

    private void FixedUpdate()
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

    private void GetTankAttributes()
    {
        movementSpeed = playerStats.GetMovementSpeed();
        rotationSpeed = playerStats.GetRotationSpeed();
    }
}
