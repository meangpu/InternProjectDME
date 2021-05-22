using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookChildSetup : MonoBehaviour
{
    public Image myImageComponent;
    public EnemyObj selfData;
    public BookClickEnemy chooseEnemyScript;

    public void showData(EnemyObj _data)
    {
        myImageComponent.sprite = _data.GetSprite()[0];
        selfData = _data;
        chooseEnemyScript = transform.parent.GetComponent<BookClickEnemy>();
    }

    public void showEnemyData()
    {
        chooseEnemyScript.updateData(selfData);
    }
}
