using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private GameObject nowtarget;
    private Transform tranTarget;
    public bool startTracking = false;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float moveSpeed;

    public void setupTrack()
    {
        rb = this.GetComponent<Rigidbody2D>();
        startTracking = true;
        nowtarget = gameObject.GetComponent<EnemyDisplay>().target;
        moveSpeed = gameObject.GetComponent<EnemyDisplay>().eneSpeed;
        tranTarget = nowtarget.transform;
    }

    private void tracking()
    {
        Vector3 direction = tranTarget.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void moveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }


    void Update()
    {
        if (startTracking)
        {
            tracking();
        }
        
    }

    private void FixedUpdate() 
    {
        moveEnemy(movement);
    }

}
