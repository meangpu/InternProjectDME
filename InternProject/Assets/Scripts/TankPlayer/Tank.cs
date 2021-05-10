using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Tank", menuName="NewTank")]
public class Tank : ScriptableObject
{
    public string tankName;
    public string tankDes;
    public Sprite artWork;
    public int hp;
    public float moveSpeed;
    public int damage;
}
