using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookClickEnemy : MonoBehaviour
{
    public Image enemyBigImage;
    public TMP_Text enemyName;
    public TMP_Text enemyDes;
    public Slider hp;
    public Slider speed;


    public void updateData(EnemyObj _data)
    {
        enemyBigImage.sprite = _data.GetSprite()[0];
        enemyName.text = _data.GetName();
        enemyDes.text = _data.GetDescription();
        hp.value = _data.GetHealth();
        speed.value = _data.GetMovementSpeed();
    }
}
