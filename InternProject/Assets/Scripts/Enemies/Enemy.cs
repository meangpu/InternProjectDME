using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ITargetable, IEnemy
{
    [SerializeField] private EnemyDisplay enemyDisplay;

    [SerializeField] private Image circleHp;
    [SerializeField] private GameObject parentHp;
    [SerializeField] private GameObject goldPfb;

    [SerializeField] bool isBoss;
    [SerializeField] Boss1 bossScript;
    [SerializeField] Animator anim;


    bool Immortal = false;

    private WaveManager waveManager;

    private void Start()
    {
        waveManager = WaveManager.Instance;
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
        anim.SetTrigger("GetHit");

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

    public void UndoImmortal()
    {
        Immortal = false;
    }

    private void EnemyDie()
    {
        PoolingSingleton.Instance.ParEnemyDeath.SpawnEnemyDeathPar(gameObject.transform.position, Quaternion.identity);
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

    public void TakeKnockback(Vector2 attackerPos, float knockbackForce = 0f)
    {
        Vector2 dirFromAttacker = ((Vector2)transform.position - attackerPos).normalized;     
        Vector2 kbForce = dirFromAttacker * knockbackForce;
        transform.position += new Vector3(kbForce.x, kbForce.y, 0);
    }

    public Transform GetTransform() => transform;

    public void Stun()
    {
        enemyDisplay.Stun();
    }

    public void Slow(float percentage, float duration)
    {
        enemyDisplay.Slow(percentage, duration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enemyDisplay.IsPassive) { return; }

        if (collision.gameObject.TryGetComponent(out IOwnedByPlayer player))
        {
            player.TakeDamage(enemyDisplay.Damage);
            EnemyDie();
        }
    }
}
