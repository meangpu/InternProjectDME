using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BaseClass : MonoBehaviour, ITargetable, IOwnedByPlayer
{
    [System.Serializable]
    public class BaseGun
    {
        public GameObject gunObj;
        public Transform gunPos;
    }

    [Header("BaseInfo")]
    public int maxHp;
    public int hp;
    public BaseGun[] guns;
    [Header("HP")]
    public Slider sliderHealth;
    public TMP_Text textHp;
    [SerializeField] GameManager gameManager;



    public void SetHealth(int health)
    {
        sliderHealth.value = health;
        textHp.text = $"{health}/{maxHp}";
    }

    public void SetMaxHealth()
    {
        sliderHealth.maxValue = maxHp;
        sliderHealth.value = maxHp;
        textHp.text = $"{maxHp}/{maxHp}";
    }

    private void Awake() 
    {
        SetMaxHealth();
        SetGun();
    }

    private void SetGun()
    {
        foreach (var gun in guns)
        {
            Instantiate(gun.gunObj, gun.gunPos.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        DamagePopup.Create(transform.position, damage, DamagePopup.DamageType.Player);
        if (hp <= 0)
        {
            hp = 0;
            DestroyBase();
        }
        SetHealth(hp);
    }

    private void DestroyBase()
    {
        gameManager.GameOver();
    }

    public Transform GetTransform() => transform;

}
