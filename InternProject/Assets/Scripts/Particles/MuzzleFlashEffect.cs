using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashEffect : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    private void OnEnable()
    {
        animator.Play("MuzzleFlash");
    }

    private void DisableEffect()
    {
        gameObject.SetActive(false);
    }
}
