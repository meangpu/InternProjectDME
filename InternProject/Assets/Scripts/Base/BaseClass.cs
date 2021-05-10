using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BaseClass : MonoBehaviour
{
    [Header("BaseInfo")]
    public int maxHp;
    public int hp;
    public GameObject[] gun;
    [Header("HP")]
    public Slider siderHealth;
    public TMP_Text textHp;

    public void SetHealth(int health)
    {
        siderHealth.value = health;
        textHp.text = $"{health}/{maxHp}";
    }

    public void SetMaxHealth()
    {
        siderHealth.maxValue = maxHp;
        siderHealth.value = maxHp;
        textHp.text = $"{maxHp}/{maxHp}";
    }

    private void Awake() 
    {
        SetMaxHealth();
    }


}
