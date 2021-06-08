using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        PoolingSingleton.Instance.ParEnemyDeath.ReturnObject(gameObject);
    }
}