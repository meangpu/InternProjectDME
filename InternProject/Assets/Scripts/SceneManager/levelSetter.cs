using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LevelInfo
{
    public string levelName;
    public Sprite levelLayout;
    [TextArea(15, 20)]
    public string levelDescription;
    public int sceneIndex;
}

public class levelSetter : MonoBehaviour
{
    [SerializeField] ObjLevel levelObj;
    [SerializeField] LevelInfo thisLevelInfo;
    [SerializeField] levelInfoPanel levelPanel;

    public void setInfoToThisLevel()
    {
        levelPanel.setLevel(thisLevelInfo);
        levelPanel.gameObject.SetActive(true);
    }

}
