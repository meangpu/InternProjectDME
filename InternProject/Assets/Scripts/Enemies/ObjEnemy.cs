using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="New Enemy", menuName="Enemy/Create New Enemy")]
public class ObjEnemy : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private string[] tags;
    [TextArea(15, 20)]
    [SerializeField] private string des;
    [SerializeField] private Sprite[] artWork;
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int EC;
    [SerializeField] private string targetTagName;
    [SerializeField] private bool isPassive;
    [SerializeField] private float attackRange;
    [SerializeField] private float atkSpeed;
    [SerializeField] private int moneyDrop;
    [SerializeField] private ObjGold[] goldDropSK;

    public string GetName() => enemyName;
    public string[] GetTags() => tags;
    public string GetDescription() => des;
    public Sprite[] GetSprite() => artWork;
    public int GetHealth() => hp;
    public float GetMovementSpeed() => moveSpeed;
    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public int GetEC() => EC;
    public string GetTargetTag() => targetTagName;
    public bool GetIsPassive() => isPassive;
    public float GetAttackRange() => attackRange;
    public float GetAtkSpeed() => atkSpeed;
    public int GetMoneyDrop() => moneyDrop;
    public ObjGold[] GetMoneyDropSK() => goldDropSK;

    public int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
}
