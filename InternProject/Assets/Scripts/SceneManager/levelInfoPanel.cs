using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class levelInfoPanel : MonoBehaviour
{
    [SerializeField] TMP_Text levelName;
    [SerializeField] Image levelImage;
    [SerializeField] TMP_Text levelDescription;
    [SerializeField] GoToScene changeSceneScpt;

    public void setLevel(ObjLevel _info)
    {
        levelName.text = _info.GetLevelName();
        levelImage.sprite = _info.GetLevelPreview();
        levelDescription.text = _info.GetLevelDescription();
        
        changeSceneScpt.destination = _info.GetLevelName();
        changeSceneScpt.sceneIndex = _info.GetSceneIndex();
    }


}


