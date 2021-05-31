using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour, ITargetable, IOwnedByPlayer
{
    [SerializeField] private PlayerStats playerStats = null;

    public void TakeDamage(int dmg)
    {
        playerStats.TakeDamage(dmg);
    }

    public Transform GetTransform() => transform;
}
