using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiSpring : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject[] enemys;


    public Vector2 GetMovementIntention (GameObject enemy)
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        // initialise intention
        Vector2 intention = Vector2.zero;

        //chase the player
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)enemy.transform.position).normalized;
        float distance = Vector2.Distance((Vector2)player.transform.position, (Vector2)enemy.transform.position);

        float targetDistance = 1f;
        float springDistance = (distance - targetDistance);
        intention += direction*springDistance;

        // spread out
        foreach (GameObject otherEnemy in enemys)
        {
            if(enemy == otherEnemy) continue;  // Don't repel self
            Vector2 _direction = ((Vector2)enemy.transform.position - (Vector2)otherEnemy.transform.position).normalized;
            float _distance = Vector2.Distance((Vector2)enemy.transform.position, (Vector2)otherEnemy.transform.position);

            float springStrenght = 1f / (1f + _distance * _distance * _distance);
            intention -= direction * springStrenght;
        }

        if (intention.magnitude < 0.8f)
        {
            return Vector2.zero;
        }


        return intention.normalized;
    }


}
