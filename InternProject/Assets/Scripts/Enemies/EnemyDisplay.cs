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
    private GameObject target;
    private EnemyTargetType tagName;
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
        //// fix
        dropGoldSK = enemy.GetMoneyDropSK();
        bulletType = enemy.GetBulletType();
        atkSpeed = enemy.GetAtkSpeed();
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
    public GameObject Target { get { return target; } }
    public EnemyTargetType TagName { get { return tagName; } }
    public int Health { get { return hp; } set { hp = value; } }
    public int MaxHealth { get { return maxhp; } }
    public Image CircleHP { get { return circleHp; } }
    public GameObject ParentHP { get { return parentHp; } }
    public int MinDamage { get { return minDamage; } }
    public int MaxDamage { get { return maxDamage; } }

    public ObjGold[] DropGoldSK { get { return dropGoldSK; } }
    public ObjEnemyBullet BulletType { get { return bulletType; } }
    public float AtkSpeed { get { return atkSpeed; } }

}
