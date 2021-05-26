using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New ObjPlayerTank", menuName="ObjPlayerTank/Create New ObjPlayerTank")]
public class ObjPlayerTank : ScriptableObject
{
    [SerializeField] private string tankName;
    [TextArea(15, 15)]
    [SerializeField] private string tankDes;
    [SerializeField] private Sprite artWork;
    [SerializeField] private int hp;
    [SerializeField] private int energy;
    [SerializeField] [Tooltip("Energy Regeneration Rate: EP/s (Supports float)")] private float energyRegenRate;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    public string GetName() => tankName;
    public string GetDescription() => tankDes;
    public Sprite GetSprite() => artWork;
    public int GetHealth() => hp;
    public int GetEnergy() => energy;
    public float GetEnergyRate() => energyRegenRate;
    public float GetMovementSpeed() => moveSpeed;
    public float GetRotationSpeed() => rotationSpeed;
}
