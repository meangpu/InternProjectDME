using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    public void RotateTurret(Enemy target)
    {
        if (target == null) { return; }

        Vector3 turretDirection = target.transform.position - transform.position;

        float angle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90), Mathf.Infinity);

    }
}
