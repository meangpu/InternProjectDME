using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class levelSetter : MonoBehaviour
{
    [SerializeField] ObjLevel levelObj;
    [SerializeField] levelInfoPanel levelPanel;
    [SerializeField] Button mainBtn;
    [SerializeField] GameObject lockIcon;
    [SerializeField] starSetter starSetScpt;

    private void Start() 
    {
        mainBtn.interactable = levelObj.GetIsUnlock();

        if (!levelObj.GetIsUnlock())
        {
            lockIcon.SetActive(true);
        }

        UpdateStarVisual();
    }

    public void setInfoToThisLevel()
    {
        levelPanel.setLevel(levelObj);
        levelPanel.gameObject.SetActive(true);
        
    }

    public void UpdateStarVisual()
    {
        starSetScpt.getStar(levelObj.GetCurrentStars());
    }

}
