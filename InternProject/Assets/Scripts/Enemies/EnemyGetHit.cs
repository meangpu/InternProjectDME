using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGetHit : MonoBehaviour, ITargetable
{
    [SerializeField] private EnemyDisplay enemyDisplay;

    [SerializeField] private Image circleHp;
    [SerializeField] private GameObject parentHp;
    [SerializeField] private GameObject goldPfb;

    [SerializeField] bool isBoss;
    [SerializeField] Boss1 bossScript;

    bool Immortal = false;

    private WaveManager waveManager;

    private void Start()
    {
        waveManager = WaveManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out TankBullet bullet))
        {
            Knockback(bullet.transform.position, bullet.knockBack + enemyDisplay.KnockBack);
            TakeDamage(bullet.Damage);
            
            bullet.DestroySelf();
        }

        if(col.gameObject.TryGetComponent(out TowerProjectile towerBullet))
        {
            // add kb from bullet เพิ่ม
            Knockback(towerBullet.transform.position, enemyDisplay.KnockBack);
            TakeDamage(towerBullet.Damage);
            towerBullet.DestroySelf();
        }
    }

    public void TakeDamage(int damage)
    {
        if (Immortal) { return; }
        if (!parentHp.gameObject.activeSelf)
        {
            parentHp.gameObject.SetActive(true);
        }
        
        DamagePopup.Create(transform.position, damage, DamagePopup.DamageType.Enemy);

        enemyDisplay.Health -= damage;

        if (isBoss)
        {
            if (!bossScript.isSecondForm)
            {
                if (enemyDisplay.Health <= (enemyDisplay.MaxHealth/2))
                {
                    Immortal = true;
                    bossScript.ChangeForm();
                }
            }

        }

        if (enemyDisplay.Health <= 0)
        {
            EnemyDie();           
        }
        UpdateHpCircle(enemyDisplay.Health, enemyDisplay.MaxHealth);
    }

    public void undoImmortal()
    {
        Immortal = false;
    }

    private void EnemyDie()
    {
        circleHp.fillAmount = 0;
        PoolingSingleton.Instance.EnemyPool.ReturnObject(gameObject);
        WaveManager.EnemyAlive.Remove(this);
        waveManager.SetEnemyLeftText();
        enabled = false;  

        foreach (ObjGold gold in enemyDisplay.DropGoldSK)
        {
            PoolingSingleton.Instance.GoldPool.SpawnGold(transform.position, Quaternion.identity, gold);
        }
    }

    private void UpdateHpCircle(float _hp, float _maxHp)
    {
        float nowvalue =  _hp/_maxHp;
        circleHp.fillAmount = nowvalue;
    }

    private void Knockback(Vector2 AttackerPos, float knockbackForce)
    {
        Vector2 dirFromAttacker = ((Vector2)transform.position - AttackerPos).normalized;     
        Vector2 kbForce = dirFromAttacker * knockbackForce;
        transform.position += new Vector3(kbForce.x, kbForce.y, 0);

        // Debug.Log($"{transform.position}, {AttackerPos}, {dirFromAttacker}");
    }

    public Transform GetTransform() => transform;
}
