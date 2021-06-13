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

    public void setLevel(LevelInfo _info)
    {
        levelName.text = _info.levelName;
        levelImage.sprite = _info.levelLayout;
        levelDescription.text = _info.levelDescription;
        
        changeSceneScpt.destination = _info.levelName;
        changeSceneScpt.sceneIndex = _info.sceneIndex;
    }


}


