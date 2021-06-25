using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffDisplayIngame : MonoBehaviour
{
    [SerializeField] private Image damageBoost = null;
    [SerializeField] private Animator damageUIAnimator = null;
    [SerializeField] private TimerSystem timerSystem = null;

    private readonly string disableState = "OnDisable";
    private readonly string enableState = "OnEnable";

    private void Start()
    {
        timerSystem.OnTimerFinished += HandleTimerFinished;
        timerSystem.OnTimerStarted += HandleTimerStarted;
    }

    private void Update()
    {
        damageBoost.fillAmount = timerSystem.GetRemainingPercentage(AbilityType.IncendiaryAmmo);
    }

    private void HandleTimerStarted(AbilityType abilityType)
    {
        switch (abilityType)
        {
            case AbilityType.IncendiaryAmmo:
                damageBoost.gameObject.transform.SetAsLastSibling();
                damageBoost.gameObject.SetActive(true);
                damageUIAnimator.SetTrigger(enableState);
                return;
        }
    }

    private void HandleTimerFinished(AbilityType abilityType)
    {
        switch (abilityType)
        {
            case AbilityType.IncendiaryAmmo:
                damageUIAnimator.SetTrigger(disableState);
                return;
        }
    }

    private void OnDestroy()
    {
        timerSystem.OnTimerFinished -= HandleTimerFinished;
        timerSystem.OnTimerStarted -= HandleTimerStarted;
    }
}
