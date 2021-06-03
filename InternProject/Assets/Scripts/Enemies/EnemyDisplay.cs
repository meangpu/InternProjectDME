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
    private float eneSpeed;
    private GameObject target;
    private EnemyTargetType tagName;
    private int hp;
    private int maxhp;
    private ObjGold[] dropGoldSK;
    [SerializeField] private Image circleHp;
    [SerializeField] private GameObject parentHp;
    private int minDamage;
    private int maxDamage;
    ObjEnemyBullet bulletType;
    float atkSpeed;
    float wantDistance;

    private bool isStunned = false;
    private float timeElapsedAfterStunned = 0f;

    private void Update()
    {
        if (!isStunned) { return; }

        RecoverFromStun(Time.deltaTime);
    }

    public void StartDisplay(ObjEnemy enemy)
    {
        eneImage.sprite = enemy.GetSprite()[Random.Range(0, enemy.GetSprite().Length)];
        maxSpeed = enemy.GetMovementSpeed();
        eneSpeed = maxSpeed;
        // tagName = enemy.GetTargetTag();
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
        // wantDistance = enemy.GetWantDistance();
        //// fix
        RefreshHitbox();
    }

    private void RefreshHitbox()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public void Stun()
    {
        eneSpeed = 0f;
        isStunned = true;
    }

    public void RecoverFromStun(float deltaTime)
    {
        timeElapsedAfterStunned += deltaTime;

        eneSpeed = Mathf.Clamp(eneSpeed + (maxSpeed * deltaTime / timeToRecoverFromStun), 0f, maxSpeed);

        if (timeElapsedAfterStunned >= timeToRecoverFromStun)
        {
            isStunned = false;
            return;
        }
    }

    public SpriteRenderer Image { get { return eneImage; } }
    public float Speed { get { return eneSpeed; } set { eneSpeed = value; } }
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
    public float WantDistance { get { return wantDistance; } }

}
