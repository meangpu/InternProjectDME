using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineExplosionEffect : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    private Pooler pooler;

    private void Start()
    {
        pooler = PoolingSingleton.Instance.MachineExplosion;
    }

    private void OnEnable()
    {
        animator.Play("MachineExplosion");
    }

    public void DisableEffect()
    {
        pooler.ReturnObject(gameObject);
    }
}
