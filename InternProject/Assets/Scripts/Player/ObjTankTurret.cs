using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "ObjPlayerTank/Create New Turret")]
public class ObjTankTurret : ScriptableObject
{
    [SerializeField] private string turretName;
    [TextArea(7, 7)]
    [SerializeField] private string description;
    [SerializeField] private Sprite artworks;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int ammoCount;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float reloadTime;
    [Range(0, 0.4f)]
    [SerializeField] private float knockBack;

    public string GetName() => turretName;
    public string GetDescription() => description;
    public Sprite GetSprite() => artworks;
    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public int GetAmmoCount() => ammoCount;
    public float GetRateOfFire() => rateOfFire;
    public float GetReloadTime() => reloadTime;
    public float GetKnockBack() => knockBack;

    public int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
}
