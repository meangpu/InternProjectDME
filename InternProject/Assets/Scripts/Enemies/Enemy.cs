using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ITargetable, IEnemy
{
    [SerializeField] private EnemyDisplay enemyDisplay;

    [SerializeField] private Image circleHp;
    [SerializeField] private GameObject parentHp;
    [SerializeField] private GameObject goldPfb;
    [SerializeField] List<ObjGold> goldToCalculate = new List<ObjGold>();
    [SerializeField] bool isBoss;
    [SerializeField] Boss1 bossScript;
    [SerializeField] Animator anim;
    [SerializeField] private float dropPercentage = 1f;


    [SerializeField] bool Immortal = false;
    [SerializeField] bool MenuImmortal = false;

    private PoolingSingleton pooler;

    public UnityEvent OnHit;

    private void Start()
    {
        pooler = PoolingSingleton.Instance;
    }

    public void TakeDamage(int damage)
    {
        if (Immortal) 
        {
            if (MenuImmortal)
            {
                return; 
            }
            DamagePopup.Create(transform.position, 0, DamagePopup.DamageType.Enemy);
            return; 
        }

        if (!parentHp.activeSelf)
        {
            parentHp.SetActive(true);
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
        OnHit?.Invoke();
    }

    public void SetImmortalState(bool state)
    {
        Immortal = state;
    }

    private void EnemyDie()
    {
        switch (enemyDisplay.EnemyType)
        {
            default:
                pooler.MachineExplosion.SpawnBasicObject(transform.position, transform.rotation);
                break;
            case EnemyType.Human:
                pooler.ParticlesHumanDeathPool.SpawnEnemyDeathPar(gameObject.transform.position, Quaternion.identity);
                break;
        }
        
        circleHp.fillAmount = 0;
        switch (enemyDisplay.EnemyId)
        {
            default:
                pooler.EnemyPool.ReturnObject(gameObject);
                break;
            case EnemyId.TolusinTank:
                pooler.EnemyBasicTankPool.ReturnObject(gameObject);
                break;
            case EnemyId.ArtilleryTank:
                pooler.EnemyArtilleryTankPool.ReturnObject(gameObject);
                break;
            case EnemyId.BomberPlane:
                pooler.EnemyBomberPlanePool.ReturnObject(gameObject);
                break;
            case EnemyId.DoomsdayTank:
                pooler.EnemyDoomsdayTankPool.ReturnObject(gameObject);
                break;
            case EnemyId.MWing:
                pooler.MWingPool.ReturnObject(gameObject);
                break;
            case EnemyId.SpyPlane:
                pooler.EnemySpyPlanePool.ReturnObject(gameObject);
                break;
            case EnemyId.Boss:
                Destroy(gameObject);

                // Remove all enemy in the list from wavemanager
                break;
        }
        
        

        List<ObjGold> coinToSpawn = CoinSpawnCalculator(enemyDisplay.DropGold);

        for (int i = 0; i < coinToSpawn.Count; i++)
        {
            SpawnInCircle(coinToSpawn.Count, i, coinToSpawn[i], 0.05f);
        }

        TrySpawnCollectible();
    }

    private void OnEnable()
    {
        WaveManager.EnemyAlive.Add(this);
    }

    private void OnDisable()
    {
        WaveManager.EnemyAlive.Remove(this);
    }

    private void TrySpawnCollectible()
    {
        float randomNumber = Random.Range(0f, 100f);

        if (randomNumber > dropPercentage) { return; }

        pooler.CollectiblePool.SpawnBasicObject(transform.position, Quaternion.identity);
    }

    void SpawnInCircle(int spawnCount, int _id, ObjGold coin, float randomMax)
    {
        int nowAngle = 360/spawnCount;
        float radius = 0.05f;
        Vector3 randomFactor = new Vector3(Random.Range(0, randomMax), Random.Range(0, randomMax), 0);
        Vector3 direction = Quaternion.Euler(0, 0, nowAngle*_id) * Vector3.right;

        Vector3 spawnPosition = transform.position + (direction * radius) + randomFactor;
        
        pooler.GoldPool.SpawnGold(spawnPosition, Quaternion.identity, coin, direction);
    }

    private List<ObjGold> CoinSpawnCalculator(int _value)
    {
        int leftValue = _value; // 53
        List<ObjGold> result = new List<ObjGold>() ;

        foreach (var gold in goldToCalculate)  // 1, 5, 10, 50
        {
            if (leftValue > gold.GetValue())
            {
                int thisCoinCount = leftValue / gold.GetValue(); // 53 / 50 = 1
                leftValue %= gold.GetValue();  // 53%50 = 3

                for (int i = 0; i < thisCoinCount; i++)
                {
                    result.Add(gold);
                }
            }
            else if (leftValue == gold.GetValue())
            {
                leftValue = 0;
                result.Add(gold);
            }
        }
        return result;
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
