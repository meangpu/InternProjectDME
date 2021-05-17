using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiSpring : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject[] enemys;


    public Vector3 GetMovementIntention (GameObject enemy)
    {
        // initialise intention
        Vector3 intention = Vector3.zero;

        //chase the player
        Vector3 direction = player.transform.position - enemy.transform.position;
        float distance = Vector3.Distance(player.transform.position, enemy.transform.position);

        float targetDistance = 1f;
        float springDistance = (distance - targetDistance);
        intention += direction*springDistance;

        // spread out
        foreach (GameObject otherEnemy in enemys)
        {
            if(enemy == otherEnemy) continue;  // Don't repel self
            Vector3 _direction = enemy.transform.position - otherEnemy.transform.position;
            float _distance = Vector3.Distance(enemy.transform.position, otherEnemy.transform.position);

            float springStrenght = 1f / (1f + _distance * _distance * _distance);
            intention -= direction * springStrenght;
        }

        if (intention.magnitude < 0.5f)
        {
            return Vector3.zero;
        }


        return intention.normalized;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
