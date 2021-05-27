using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private string nowtarget;
    private Transform tranTarget;
    private GameObject target;
    public bool startTracking = false;
    
    [SerializeField] float rotateAngleAdd;
    [SerializeField] private Rigidbody2D rb;
    private float moveSpeed;
    [SerializeField] EnemyAiSpring springAI;
    // public Transform child;
    

    [ContextMenu("sdasd")]
    public void SetupTrack()
    {
        // rb = this.GetComponent<Rigidbody2D>();
        EnemyDisplay enemy = gameObject.GetComponent<EnemyDisplay>();

        startTracking = true;
        nowtarget = enemy.TagName;
        target = GameObject.FindGameObjectsWithTag(nowtarget)[0];

        moveSpeed = enemy.Speed;
        // tranTarget = nowtarget.transform;
        tranTarget = target.transform;
    }

    private void AISpringMove()
    {
        rb.MovePosition((Vector2)transform.position + (springAI.GetMovementIntention(gameObject) * moveSpeed * Time.deltaTime));
    }

    private void Tracking()
    {
        Vector3 direction = tranTarget.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle+rotateAngleAdd;

    }


    private void Update() 
    {
        if (startTracking)
        {
            Tracking();
        }

    }

    private void FixedUpdate() 
    {
        AISpringMove();

    }

}
