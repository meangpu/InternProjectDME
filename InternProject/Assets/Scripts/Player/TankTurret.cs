using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Tank/Create New Turret")]
public class TankTurret : ScriptableObject
{
    [SerializeField] private string turretName;
    [SerializeField] private string description;
    [SerializeField] private Sprite artworks;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int ammoCount;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float reloadTime;

    public string GetName() => turretName;
    public string GetDescription() => description;
    public Sprite GetSprite() => artworks;
    public int GetMinDamage() => minDamage;
    public int GetMaxDamage() => maxDamage;
    public int GetAmmoCount() => ammoCount;
    public float GetRateOfFire() => rateOfFire;
    public float GetReloadTime() => reloadTime;

    public int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
}
