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

    private void Start() 
    {
        mainBtn.interactable = levelObj.GetIsUnlock();
    }

    public void setInfoToThisLevel()
    {
        levelPanel.setLevel(levelObj);
        levelPanel.gameObject.SetActive(true);
        
    }

}
