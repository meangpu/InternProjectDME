using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="newEnemy", menuName="NewEnemy")]
public class EnemyObj : ScriptableObject
{
    public string enemyName;
    public string des;
    public Sprite artWork;
    public int hp;
    public float moveSpeed;
    public int damage;
}
