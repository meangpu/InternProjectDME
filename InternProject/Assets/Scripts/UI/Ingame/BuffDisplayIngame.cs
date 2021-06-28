using UnityEngine;
using UnityEngine.UI;

public class BuffDisplayIngame : MonoBehaviour
{
    [SerializeField] private Image damageBoost = null;
    [SerializeField] private Animator damageUIAnimator = null;
    [SerializeField] private Image speedBoost = null;
    [SerializeField] private Animator speedUIAnimator = null;
    [SerializeField] private Image orbBoost = null;
    [SerializeField] private Animator orbUIAnimator = null;
    [SerializeField] private Image magnetBoost = null;
    [SerializeField] private Animator magnetUIAnimator = null;
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
        speedBoost.fillAmount = timerSystem.GetRemainingPercentage(AbilityType.SpeedBoost);
        orbBoost.fillAmount = timerSystem.GetRemainingPercentage(AbilityType.EnergyOrb);
        magnetBoost.fillAmount = timerSystem.GetRemainingPercentage(AbilityType.Magnet);
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

            case AbilityType.SpeedBoost:
                if (timerSystem.GetRemainingPercentage(AbilityType.SpeedBoost) == 0) { return; }

                speedBoost.gameObject.transform.SetAsLastSibling();
                speedBoost.gameObject.SetActive(true);
                speedUIAnimator.SetTrigger(enableState);
                return;

            case AbilityType.EnergyOrb:
                orbBoost.gameObject.transform.SetAsLastSibling();
                orbBoost.gameObject.SetActive(true);
                orbUIAnimator.SetTrigger(enableState);
                return;

            case AbilityType.Magnet:
                magnetBoost.gameObject.transform.SetAsLastSibling();
                magnetBoost.gameObject.SetActive(true);
                magnetUIAnimator.SetTrigger(enableState);
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

            case AbilityType.SpeedBoost:
                speedUIAnimator.SetTrigger(disableState);
                return;

            case AbilityType.EnergyOrb:
                orbUIAnimator.SetTrigger(disableState);
                return;

            case AbilityType.Magnet:
                magnetUIAnimator.SetTrigger(disableState);
                return;
        }
    }

    private void OnDestroy()
    {
        timerSystem.OnTimerFinished -= HandleTimerFinished;
        timerSystem.OnTimerStarted -= HandleTimerStarted;
    }
}
