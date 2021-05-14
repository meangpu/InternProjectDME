using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Tank", menuName="Tank/Create New Tank")]
public class Tank : ScriptableObject
{
    public string tankName;
    public string tankDes;
    public Sprite artWork;
    public int hp;
    public int damage;
    public int ammoCount;
    public float rateOfFire;
    public float reloadTime;
    public float moveSpeed;
    public float rotationSpeed;
}
