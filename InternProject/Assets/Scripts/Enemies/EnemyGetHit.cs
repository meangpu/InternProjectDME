using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGetHit : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private EnemyDisplay enemyDisplay;

    [SerializeField] private Image circleHp;
    [SerializeField] private GameObject parentHp;

    private WaveManager waveManager;

    private void Awake() 
    {
        waveManager = GameObject.Find("waveManager").GetComponent<WaveManager>(); //// fix this 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out TankBullet bullet))
        {
            TakeDamage(bullet.Damage);
            bullet.DestroySelf();
        }

        if(col.gameObject.TryGetComponent(out TowerProjectile towerBullet))
        {
            TakeDamage(towerBullet.Damage);
            towerBullet.DestroySelf();
        }
    }

    private void TakeDamage(int damage)
    {
        if (!parentHp.gameObject.activeSelf)
        {
            parentHp.gameObject.SetActive(true);
        }
        
        DamagePopup.Create(transform.position, damage);
        enemyDisplay.Health -= damage;



        if (enemyDisplay.Health <= 0)
        {
            circleHp.fillAmount = 0;
            PoolingSingleton.Instance.EnemyPool.ReturnObject(gameObject);
            WaveManager.EnemyAlive--;
            waveManager.SetEnemyLeftText();
            this.enabled = false;
        }
        UpdateHpCircle(enemyDisplay.Health, enemyDisplay.MaxHealth);

    }

    // private void updateSlider(int _hp)
    // {
    //     hpSlider.value = _hp;
    // }

    private void UpdateHpCircle(float _hp, float _maxHp)
    {
        float nowvalue =  _hp/_maxHp;
        circleHp.fillAmount = nowvalue;
    }

}
