using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoSetter : MonoBehaviour
{
    [SerializeField] GameObject parentInfo;
    [SerializeField] TMP_Text enemyName;
    [SerializeField] TMP_Text enemyCount;
    [SerializeField] Image enemyImage;


    public void ShowData(ObjEnemy _enemyObj, int _count)
    {
        parentInfo.SetActive(true);
        enemyImage.sprite = _enemyObj.GetSprite()[0];
        enemyName.text = _enemyObj.GetName();
        enemyCount.text = $"*{_count.ToString()}";
    }
}
