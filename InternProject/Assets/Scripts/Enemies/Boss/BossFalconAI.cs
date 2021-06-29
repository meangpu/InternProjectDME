using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFalconAI : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private EnemyDisplay enemyDisplay = null;
    [SerializeField] private float aiRepathTime = 0.5f;

    private Transform playerBase;
    private Transform player;
    private Transform currentTarget;

    private float aiTimer = 0f;

    private bool targetPlayer = true;

    private void Awake()
    {
        playerBase = GameObject.FindGameObjectWithTag("Base").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentTarget = player;
    }

    private void Update()
    {
        TryTargetPlayer();
        aiTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();

        if (aiTimer < aiRepathTime) { return; }

        aiTimer = 0f;
        LookAtTarget();
    }

    private void Move()
    {
        rb.velocity = transform.right * enemyDisplay.Speed;
    }

    private void TryTargetPlayer()
    {
        if (player.gameObject.activeInHierarchy)
        {
            if (!targetPlayer) { return; }

            currentTarget = player;
            targetPlayer = false;
        }
        else
        {
            if (targetPlayer) { return; }

            currentTarget = playerBase;
            targetPlayer = true;
        } 
    }

    private void LookAtTarget()
    {
        Vector2 direction = currentTarget.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), enemyDisplay.Speed);
    }
}
