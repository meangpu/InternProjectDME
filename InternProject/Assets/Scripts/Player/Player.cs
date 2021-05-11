using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private Rigidbody2D modelRb = null;
    [SerializeField] private float movementSpeed = 5f; // Read from ScriptableObjects later
    [SerializeField] private float rotationSpeed = 3f;

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
        } else
        {
            StopMovement();
        }

        if (Mathf.Abs(rotateDirection) >= controllerDeadZone)
        {
            Rotate();
        } else
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
        rb.velocity = (Vector2)transform.up * -moveDirection * movementSpeed;
    }

    private void Rotate()
    {
        //modelRb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -rotateDirection * rotationSpeed));
        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
    }

    private void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    private void StopRotation()
    {
        //modelRb.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.zero));
    }
}

