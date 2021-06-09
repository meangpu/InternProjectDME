using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer eneImage;
    [SerializeField] private float timeToRecoverFromStun = 5f;

    // [SerializeField] ObjEnemy enemyScriptableObj;
    private float maxSpeed;
    private float enemySpeed;
    private int hp;
    private int maxhp;
    private ObjGold[] dropGoldSK;
    [SerializeField] private Image circleHp;
    [SerializeField] private GameObject parentHp;
    private int minDamage;
    private int maxDamage;
    private ObjEnemyBullet bulletType;
    private float atkSpeed;
    private float slowDuration;
    private float attackRange;
    private float bulletSpeed;
    private float bulletLifetime;
    private int goldDrop;

    private bool isPassive;

    private bool isStunned = false;
    private bool isSlowed = false;
    private float timeElapsedAfterStunned = 0f;

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        RecoverFromSlow(deltaTime);
        RecoverFromStun(deltaTime);
    }

    private void OnEnable()
    {
        isStunned = false;
        isSlowed = false;
    }

    public void StartDisplay(ObjEnemy enemy)
    {
        eneImage.sprite = enemy.GetSprite()[Random.Range(0, enemy.GetSprite().Length)];
        maxSpeed = enemy.GetMovementSpeed();
        enemySpeed = maxSpeed;
        hp = enemy.GetHealth();
        maxhp = enemy.GetHealth();
        minDamage = enemy.GetMinDamage();
        maxDamage = enemy.GetMaxDamage();
        circleHp.fillAmount = 1;
        parentHp.gameObject.SetActive(false);
        isPassive = enemy.GetIsPassive();
        dropGoldSK = enemy.GetMoneyDropSK();
        bulletType = enemy.GetBulletType();
        atkSpeed = enemy.GetAtkSpeed();
        attackRange = enemy.GetAttackRange();
        bulletSpeed = enemy.GetBulletSpeed();
        bulletLifetime = enemy.GetBulletLifetime();
        goldDrop = enemy.GetMoneyDrop();
    }

    public void Slow(float percentage, float duration)
    {
        enemySpeed = ((100 - percentage) * enemySpeed / 100);
        slowDuration = duration;
        isSlowed = true;
    }

    private void RecoverFromSlow(float deltaTime)
    {
        if (!isSlowed) { return; }

        slowDuration -= deltaTime;

        if (slowDuration > 0) { return; }

        enemySpeed = maxSpeed;
        isSlowed = false;
    }

    public void Stun()
    {
        enemySpeed = 0f;
        isStunned = true;
    }

    public void RecoverFromStun(float deltaTime)
    {
        if (!isStunned) { return; }

        timeElapsedAfterStunned += deltaTime;

        enemySpeed = Mathf.Clamp(enemySpeed + (maxSpeed * deltaTime / timeToRecoverFromStun), 0f, maxSpeed);

        if (timeElapsedAfterStunned >= timeToRecoverFromStun)
        {
            isStunned = false;
            return;
        }
    }

    public SpriteRenderer Image { get { return eneImage; } }
    public float Speed { get { return enemySpeed; } set { enemySpeed = value; } }
    public int Health { get { return hp; } set { hp = value; } }
    public int MaxHealth { get { return maxhp; } }
    public Image CircleHP { get { return circleHp; } }
    public GameObject ParentHP { get { return parentHp; } }
    public int Damage { get { return Random.Range(minDamage, maxDamage + 1); } }
    public bool IsPassive { get { return isPassive; } }
    public ObjGold[] DropGoldSK { get { return dropGoldSK; } }
    public ObjEnemyBullet BulletType { get { return bulletType; } }
    public float AtkSpeed { get { return atkSpeed; } }
    public float AttackRange { get { return attackRange; } }
    public float BulletSpeed { get { return bulletSpeed; } }
    public float BulletLifetime { get { return bulletLifetime; } }
    public int DropGold { get { return goldDrop; } }
}
