using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    // Create a target and targetable script
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 200f;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {   
        AimAtTarget();
    }

    private void AimAtTarget()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotationSpeed;
        
        rb.velocity = (Vector2)transform.up * movementSpeed;
    }

    
    /*  Vector3 direction = target.position - transform.position;

        float aimAtAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, aimAtAngle), rotationSpeed);*/
}
