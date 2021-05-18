using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGetHit : MonoBehaviour
{
    private Pooler pool;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private EnemyDisplay enemyDisplay;

    [SerializeField] private Image circleHp;
    [SerializeField] private GameObject parentHp;

    private void Start() 
    {
        pool = transform.parent.GetComponent<Pooler>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("PlayerBullet"))
        {
            TankBullet bullet = col.gameObject.GetComponent<TankBullet>();
            TakeDamage(bullet.damage);
            bullet.DestroySelf();
        }
        if(col.gameObject.CompareTag("TowerBullet"))
        {
            TowerProjectile bullet = col.gameObject.GetComponent<TowerProjectile>();
            TakeDamage(bullet.damage);
            bullet.DestroySelf();
        }
    }

    private void TakeDamage(int damage)
    {
        if (!parentHp.gameObject.activeSelf)
        {
            parentHp.gameObject.SetActive(true);
        }
        
        enemyDisplay.hp -= damage;


        if (enemyDisplay.hp <= 0)
        {
            circleHp.fillAmount = 0;
            pool.ReturnObject(gameObject);
        }
        updateHpCircle(enemyDisplay.hp, enemyDisplay.maxhp);

    }

    // private void updateSlider(int _hp)
    // {
    //     hpSlider.value = _hp;
    // }

    private void updateHpCircle(float _hp, float _maxHp)
    {
        float nowvalue =  _hp/_maxHp;
        circleHp.fillAmount = nowvalue;
    }

}
