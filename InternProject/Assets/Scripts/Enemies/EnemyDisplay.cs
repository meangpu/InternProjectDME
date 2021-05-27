using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer eneImage;
    // [SerializeField] ObjEnemy enemyScriptableObj;
    private float eneSpeed;
    private GameObject target;
    private string tagName;
    private int hp;
    private int maxhp;

    private ObjGold[] dropGoldSK;

    [SerializeField] private Image circleHp;
    [SerializeField] private GameObject parentHp;
    private int minDamage;
    private int maxDamage;
    private float knockBack;

    // private void Start() 
    // {
    //     if (enemyScriptableObj != null)
    //     {
    //         StartDisplay(enemyScriptableObj);
    //     }
    // }

    public void StartDisplay(ObjEnemy enemy)
    {
        eneImage.sprite = enemy.GetSprite()[Random.Range(0, enemy.GetSprite().Length)];
        eneSpeed = enemy.GetMovementSpeed();
        tagName = enemy.GetTargetTag();
        hp = enemy.GetHealth();
        maxhp = enemy.GetHealth();
        minDamage = enemy.GetMinDamage();
        maxDamage = enemy.GetMaxDamage();
        circleHp.fillAmount = 1;
        parentHp.gameObject.SetActive(false);
        //// fix
        dropGoldSK = enemy.GetMoneyDropSK();
        //// fix
        refreshHitbox();
    }

    private void refreshHitbox()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public SpriteRenderer Image { get { return eneImage; } }
    public float Speed { get { return eneSpeed; } }
    public GameObject Target { get { return target; } }
    public string TagName { get { return tagName; } }
    public int Health { get { return hp; } set { hp = value; } }
    public int MaxHealth { get { return maxhp; } }
    public Image CircleHP { get { return circleHp; } }
    public GameObject ParentHP { get { return parentHp; } }
    public int MinDamage { get { return minDamage; } }
    public int MaxDamage { get { return maxDamage; } }

    public ObjGold[] DropGoldSK { get { return dropGoldSK; } }
    public float KnockBack { get { return knockBack; } }


}
