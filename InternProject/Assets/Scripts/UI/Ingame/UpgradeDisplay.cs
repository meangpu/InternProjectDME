using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private GameObject popupPanel = null;

    public enum UpgradeType
    {
        Tank,
        Gun
    }

    private void OnDisable()
    {
        popupPanel.SetActive(false);
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
        popupPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popupPanel.SetActive(false);
    }
}
