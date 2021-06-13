using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffects : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    private void OnEnable()
    {
        animator.Play("Gray_Explosion");
    }

    public void DisableEffect()
    {
        PoolingSingleton.Instance.BulletExplosion.ReturnObject(gameObject);
    }
}
