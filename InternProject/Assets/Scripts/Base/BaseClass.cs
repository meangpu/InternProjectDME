using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BaseClass : MonoBehaviour
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
    public Slider siderHealth;
    public TMP_Text textHp;
    [SerializeField] GameManager gameManager;



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
        SetGun();
    }

    private void SetGun()
    {
        foreach (var gun in guns)
        {
            Instantiate(gun.gunObj, gun.gunPos.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out BulletEnemy bullet))
        {
            TakeDamage(bullet.Damage);
            bullet.DestroySelf();
        }
    }


    void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            DestroyBase();
        }
        SetHealth(hp);
        
    }

    void DestroyBase()
    {
        gameManager.GameOver();
    }

}
