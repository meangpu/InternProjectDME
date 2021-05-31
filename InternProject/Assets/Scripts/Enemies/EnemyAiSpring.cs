using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiSpring : MonoBehaviour
{
    GameObject[] enemys;


    public Vector2 GetMovementIntention (GameObject enemy, GameObject _target, float _wantDistance)
    {

        enemys = GameObject.FindGameObjectsWithTag("Enemy");

        // initialise intention
        Vector2 intention = Vector2.zero;


        Vector2 direction = ((Vector2)_target.transform.position - (Vector2)enemy.transform.position).normalized;
        float distance = Vector2.Distance((Vector2)_target.transform.position, (Vector2)enemy.transform.position);
        float targetDistance = _wantDistance;
        float springDistance = (distance - targetDistance);
        intention += direction*springDistance;


        // spread out
        foreach (GameObject otherEnemy in enemys)
        {
            if(enemy == otherEnemy) continue;  // Don't repel self
            Vector2 _direction = ((Vector2)enemy.transform.position - (Vector2)otherEnemy.transform.position).normalized;
            float _distance = Vector2.Distance((Vector2)enemy.transform.position, (Vector2)otherEnemy.transform.position);

            float springStrenght = 1f / (1f + _distance * _distance * _distance);
            intention += _direction * springStrenght;
        }

        if (intention.magnitude < 0.3f)
        {
            return Vector2.zero;
        }


        return intention.normalized;
    }


}
