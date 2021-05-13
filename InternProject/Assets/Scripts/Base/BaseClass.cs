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
        setGun();
    }

    private void setGun()
    {
        foreach (var gun in guns)
        {
            Instantiate(gun.gunObj, gun.gunPos.position, Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "EnemyBullet")
        {
            takeDamage(col.gameObject.GetComponent<BulletEnemy>().damage);
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyBullet")
        {
            takeDamage(col.gameObject.GetComponent<BulletEnemy>().damage);
            Destroy(col.gameObject);
        }
    }


    void takeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            gotDestroy();
        }
        SetHealth(hp);
        
    }

    void gotDestroy()
    {
        // Debug.Log("Base got Destroy!!");
    }

}
