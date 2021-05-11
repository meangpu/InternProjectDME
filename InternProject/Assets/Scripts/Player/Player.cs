using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Transform gun = null;
    [SerializeField] private Transform barrel = null;
    [SerializeField] private float movementSpeed = 5f; // Read from ScriptableObjects later
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private Tank tank = null;

    [Header("Temp Fields")]
    [SerializeField] private GameObject bulletPrefab = null;

    [Header("Input Settings")]
    [SerializeField] private float controllerDeadZone = 0.1f;

    private PlayerControls playerControls;
    private float moveDirection;
    private float rotateDirection;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Tank.Shoot.performed += _ => Shoot();
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
        ReadInputValues();

        // If the inputs are less than the dead zone, ignore them.
        if (Mathf.Abs(moveDirection) >= controllerDeadZone)
        {
            Move();
        }
        else
        {
            StopMovement();
        }

        if (Mathf.Abs(rotateDirection) >= controllerDeadZone)
        {
            Rotate();
        }
        else
        {
            StopRotation();
        }
    }

    private void ReadInputValues()
    {
        moveDirection = playerControls.Tank.Move.ReadValue<float>();
        rotateDirection = playerControls.Tank.Rotate.ReadValue<float>();
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.up * -moveDirection * tank.moveSpeed;
    }

    private void Rotate()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -rotateDirection * rotationSpeed));
        // playerTank.Rotate(Vector3.forward * -rotateDirection * rotationSpeed);
    }

    private void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    private void StopRotation()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.zero));
        // playerTank.Rotate(Vector3.zero);
    }

    private void Shoot() 
    {
        Instantiate(bulletPrefab, barrel.position, barrel.rotation);
    }
}

