using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AddonsModuleSetup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;

    [Header("Sounds")]
    [SerializeField] private ObjSound hoverSound = null;
    [SerializeField] private ObjSound onExitSound = null;
    [SerializeField] private AssignSound assignSound = null;

    [Header("selfValue")]
    [SerializeField] GameObject LockPanel;
    [SerializeField] Button lockButton;
    [SerializeField] GameObject glowCanbuy;
    [SerializeField] TMP_Text unlockPriceText;
    [SerializeField] ParticleSystem unlockPar;

    private AddonsUIManager uiManager;
    private ObjAbility addonObject;

    private void Awake()
    {
        uiManager = transform.parent.GetComponent<AddonsUIManager>();
    }

    public void DisplayData(ObjAbility addon)
    {
        addonObject = addon;
        image.sprite = addonObject.GetIcon();

        if (addonObject.GetIsUnlock())
        {
            // if tank is unlocked disable tank panel
            LockPanel.SetActive(false);
        }
        else
        {
            unlockPriceText.text = addonObject.GetBuyStarPrice().ToString();
            if (starManager.Instance.getNowStar() >= addonObject.GetBuyStarPrice())
            {
                // player cannot click to open buy panel if they don't have enough star
                lockButton.interactable = true;
                glowCanbuy.SetActive(true);
            }
            else
            {
                lockButton.interactable = false;
                glowCanbuy.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        assignSound.SetSound(hoverSound);
        assignSound.PlaySound();
        uiManager.UpdateDescription(addonObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        assignSound.SetSound(onExitSound);
        assignSound.PlaySound();
        uiManager.HideDescription();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiManager.SelectAddonToAssign(addonObject);
    }

    public void showAskBuy()
    {
        starManager.Instance.showAskAddonPanel(addonObject.GetBuyStarPrice(), addonObject, this);
    }

    public void unlockThisAddon()
    {
        LockPanel.SetActive(false);
        unlockPar.Play();
    }
}
