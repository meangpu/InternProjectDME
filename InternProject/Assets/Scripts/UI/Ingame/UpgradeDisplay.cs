using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UpgradeType upgradeType;

    public enum UpgradeType
    {
        Tank,
        Gun
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (upgradeType)
        {
            case UpgradeType.Tank:
                PlayerStats.Instance.TankLevelUp();
                break;

            case UpgradeType.Gun:
                PlayerStats.Instance.GunLevelUp();
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show stats by type
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide stats
    }
}
