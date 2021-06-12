using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class callWaveEarly : MonoBehaviour
{
    [SerializeField] GameObject parentInfo;
    [SerializeField] TMP_Text enemyName;
    [SerializeField] TMP_Text enemyCount;
    [SerializeField] Image enemyImage;

    // [HideInInspector]
    public ObjEnemy nowEnemyObj;
    [HideInInspector]
    public int nowEnemyCount;


    public void ShowData()
    {
        parentInfo.SetActive(true);
        enemyImage.sprite = nowEnemyObj.GetSprite()[0];
        enemyName.text = nowEnemyObj.GetName();
        enemyCount.text = $"*{nowEnemyCount.ToString()}";
    }

}
