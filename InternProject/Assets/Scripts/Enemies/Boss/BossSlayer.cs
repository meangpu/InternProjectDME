using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlayer : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI = null;
    [SerializeField] private float shieldActivationTime = 3f;
    [SerializeField] private float shieldCooldown = 15f;
    [SerializeField] private float missileCooldown = 5f;

    private float shieldTime = 0f;
    private float shieldCooldownTime = 0f;

    private bool isShieldActivated = false;

    private void Start()
    {
        enemyAI.Setup();
    }

    private void Update()
    {
        ProcessCooldowns(Time.deltaTime);
        TryDisableShield();
    }

    private void ProcessCooldowns(float deltaTime)
    {
        shieldTime = Mathf.Max(shieldTime - deltaTime, 0f);
        shieldCooldownTime = Mathf.Max(shieldCooldownTime - deltaTime, 0f);
    }

    private void TryDisableShield()
    {
        if (shieldTime > 0f) { return; }

        if (!isShieldActivated) { return; }

        PutShieldOnCooldown();
    }

    private void PutShieldOnCooldown()
    {
        isShieldActivated = false;
        shieldCooldownTime = shieldCooldown;
    }

    private void ActivateShield()
    {
        isShieldActivated = true;
        shieldTime = shieldActivationTime;
    }
}
