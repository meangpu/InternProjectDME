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
    [SerializeField] private GameObject goldPfb;

    private WaveManager waveManager;

    private void Awake() 
    {
        waveManager = WaveManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out TankBullet bullet))
        {
            knockBack(bullet.transform.position, 0.25f);
            TakeDamage(bullet.Damage);
            
            bullet.DestroySelf();
        }

        if(col.gameObject.TryGetComponent(out TowerProjectile towerBullet))
        {
            knockBack(towerBullet.transform.position, 0.1f);
            TakeDamage(towerBullet.Damage);
            towerBullet.DestroySelf();

        }
    }

    public void TakeDamage(int damage)
    {
        if (!parentHp.gameObject.activeSelf)
        {
            parentHp.gameObject.SetActive(true);
        }
        
        DamagePopup.Create(transform.position, damage, "Enemy");

        enemyDisplay.Health -= damage;

        if (enemyDisplay.Health <= 0)
        {
            EnemyDie();           
        }
        UpdateHpCircle(enemyDisplay.Health, enemyDisplay.MaxHealth);

    }

    private void EnemyDie()
    {
        circleHp.fillAmount = 0;
        PoolingSingleton.Instance.EnemyPool.ReturnObject(gameObject);
        WaveManager.EnemyAlive.Remove(this);
        waveManager.SetEnemyLeftText();
        this.enabled = false;  

        foreach (ObjGold ObjGold in enemyDisplay.DropGoldSK)
        {
            PoolingSingleton.Instance.GoldPool.SpawnGold(transform.position, Quaternion.identity, ObjGold);
        }
    }

    private void UpdateHpCircle(float _hp, float _maxHp)
    {
        float nowvalue =  _hp/_maxHp;
        circleHp.fillAmount = nowvalue;
    }

    private void knockBack(Vector2 AttackerPos, float knockbackForce)
    {
        Vector2 dirFromAttacker = ((Vector2)transform.position - AttackerPos).normalized;     
        Vector2 kbForce = dirFromAttacker * knockbackForce;
        transform.position += new Vector3(kbForce.x, kbForce.y, 0);

        // Debug.Log($"{transform.position}, {AttackerPos}, {dirFromAttacker}");
    }
}
