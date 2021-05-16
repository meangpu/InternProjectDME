using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    public SpriteRenderer eneImage;
    public float eneSpeed;
    public GameObject target;
    public string tagName;
    public int hp;
    public int maxhp;
    public Image circleHp;
    [SerializeField] private Slider hpSlider;

    public void StartDisplay(EnemyObj enemy)
    {
        eneImage.sprite = enemy.GetSprite();
        eneSpeed = enemy.GetMovementSpeed();
        tagName = enemy.GetTargetTag();
        hp = enemy.GetHealth();
        maxhp = enemy.GetHealth();
        circleHp.fillAmount = 1;
        // hpSlider.maxValue = enemy.GetHealth();
        // hpSlider.value = enemy.GetHealth();
    }


}
