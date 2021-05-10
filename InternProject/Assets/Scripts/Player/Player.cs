using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float movementSpeed = 5f; // Read from ScriptableObjects later

    [Header("Input Settings")]
    [SerializeField] private float controllerDeadZone = 0.1f;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Tank.Move.performed += _ => Move();
        playerControls.Tank.Move.canceled += _ => ResetMovement();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        
    }

    private void Move()
    {
        float moveDirection = playerControls.Tank.Move.ReadValue<float>();

        // If the input is less than the dead zone, ignore it.
        if (Mathf.Abs(moveDirection) < controllerDeadZone) { return; }

        rb.velocity = new Vector2(rb.velocity.x, moveDirection * movementSpeed);
    }

    private void ResetMovement()
    {
        rb.velocity = new Vector2(rb.velocity.x, Vector2.zero.y);
    }
}
