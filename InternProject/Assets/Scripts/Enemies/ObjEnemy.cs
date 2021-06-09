using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="New Enemy", menuName="Enemy/Create New Enemy")]
public class ObjEnemy : ScriptableObject
{
    [SerializeField] private string enemyName;
    [TextArea(15, 20)]
    [SerializeField] private string des;
    [SerializeField] private Sprite[] artWork;
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int EC;
    [SerializeField] private bool isPassive;
    [SerializeField] private float attackRange;
    [Tooltip("how many attack per second")]
    [SerializeField] private float atkSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime;
    [SerializeField] ObjEnemyBullet bulletType;
    [SerializeField] private int moneyDrop;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private bool isFlying;
    
    public string GetName() => enemyName;
    public string GetDescription() => des;
    public Sprite[] GetSprite() => artWork;
    public int GetHealth() => hp;
    public float GetMovementSpeed() => moveSpeed;
    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public int GetEC() => EC;
    public bool GetIsPassive() => isPassive;
    public float GetAttackRange() => attackRange;
    public float GetAtkSpeed() => atkSpeed;
    public int GetMoneyDrop() => moneyDrop;

    public float GetBulletLifetime() => bulletLifetime;
    public float GetBulletSpeed() => bulletSpeed;
    public ObjEnemyBullet GetBulletType() => bulletType;
    public EnemyType GetEnemyType() => enemyType;
    public bool GetIsFlying() => isFlying;

}
