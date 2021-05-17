using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPointTransform = null;

    private Vector3 projectileSpawnPoint;

    private void Awake()
    {
        projectileSpawnPoint = projectileSpawnPointTransform.position;
    }

}
