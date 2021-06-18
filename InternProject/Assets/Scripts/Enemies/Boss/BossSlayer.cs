using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlayer : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI = null;

    private void Start()
    {
        enemyAI.Setup();
    }
}
