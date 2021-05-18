using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    [SerializeField] private TowerAI AI = null;

    private Enemy target;

    private void Update()
    {
        RotateTurret();
    }

    private void RotateTurret()
    {
        target = AI.GetTarget();

        if (target != null)
        {
            Vector3 turretDirection = target.transform.position - transform.position;

            float angle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90), Mathf.Infinity);
        }
    }
}
