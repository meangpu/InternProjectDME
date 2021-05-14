using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="New Enemy", menuName="Enemy/Create New Enemy")]
public class EnemyObj : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private string[] tags;
    [SerializeField] private string des;
    [SerializeField] private Sprite artWork;
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private int EC;
    [SerializeField] private string targetTagName;
    [SerializeField] private bool isPassive;
    [SerializeField] private float attackRange;

    public string GetName() => enemyName;
    public string[] GetTags() => tags;
    public string GetDescription() => des;
    public Sprite GetSprite() => artWork;
    public int GetHealth() => hp;
    public float GetMovementSpeed() => moveSpeed;
    public int GetDamage() => damage;
    public int GetEC() => EC;
    public string GetTargetTag() => targetTagName;
    public bool GetIsPassive() => isPassive;
    public float GetAttackRange() => attackRange;
}
