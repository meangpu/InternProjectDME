using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Tank", menuName="Tank/Create New Tank")]
public class Tank : ScriptableObject
{
    [SerializeField] private string tankName;
    [SerializeField] private string tankDes;
    [SerializeField] private Sprite artWork;
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    public string GetName() => tankName;
    public string GetDescription() => tankDes;
    public Sprite GetSprite() => artWork;
    public int GetHealth() => hp;
    public float GetMovementSpeed() => moveSpeed;
    public float GetRotationSpeed() => rotationSpeed;
}
