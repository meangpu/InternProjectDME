using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    [SerializeField] private PlayerAbilities playerAbilities = null;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = PlayerStats.Instance;

        playerStats.OnEnergyShieldDisabled += HandleDisableEnergyShield;
        playerAbilities.OnTriggerEnergyShield += HandleEnableEnergyShield;
    }

    private void HandleEnableEnergyShield()
    {
        Debug.Log("ENABLE");
        gameObject.SetActive(true);
    }

    private void HandleDisableEnergyShield()
    {
        Debug.Log("DISABLE");
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        playerStats.OnEnergyShieldDisabled -= HandleDisableEnergyShield;
        playerAbilities.OnTriggerEnergyShield -= HandleEnableEnergyShield;
    }
}
