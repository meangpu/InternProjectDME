using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddonsUIManager : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private TMP_Text addonName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private TMP_Text energyCostText;
    [SerializeField] private AddonsSelectionInputManager inputManager;

    [Header("Sounds")]
    [SerializeField] private AssignSound assignSound = null;
    [SerializeField] private ObjSound onClickSound = null;
    [SerializeField] private ObjSound onCancelSound = null;
    [SerializeField] private ObjSound onConfirmSound = null;

    private void Start()
    {
        HideDescription();

        inputManager.OnConfirm += HandleConfirm;
        inputManager.OnChoose += HandleOnChoose;
        inputManager.OnCancel += HandleOnCancel;
    }

    public void UpdateDescription(ObjAbility addon)
    {
        ShowDescription();
        addonName.text = addon.GetName();
        description.text = addon.GetDescription();
        cooldownText.text = $"Cooldown: {addon.GetCooldown()} seconds";
        energyCostText.text = $"Energy Cost: {addon.GetEnergyCost()}";
    }

    public void HideDescription()
    {
        parent.SetActive(false);
    }

    public void ShowDescription()
    {
        parent.SetActive(true);
    }

    public void SelectAddonToAssign(ObjAbility ability)
    {
        inputManager.gameObject.SetActive(true);
        HideDescription();
        inputManager.PrepareForAbilityAssignment(ability);
    }

    private void HandleConfirm()
    {
        assignSound.SetAndPlaySound(onConfirmSound);
    }

    private void HandleOnChoose()
    {
        assignSound.SetAndPlaySound(onClickSound);
    }

    private void HandleOnCancel()
    {
        assignSound.SetAndPlaySound(onCancelSound);
    }

    private void OnDestroy()
    {
        inputManager.OnConfirm -= HandleConfirm;
        inputManager.OnChoose -= HandleOnChoose;
        inputManager.OnCancel -= HandleOnCancel;
    }
}
