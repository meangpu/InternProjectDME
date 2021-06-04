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
    [SerializeField] private int[] minDamage;
    [SerializeField] private int[] maxDamage;
    [SerializeField] private int[] ammoCount;
    [SerializeField] private float[] rateOfFire;
    [SerializeField] private float[] reloadTime;
    [SerializeField] private float[] bulletSpeed;
    [SerializeField] private float lifetime;
    [Range(0, 0.4f)]
    [SerializeField] private float knockBack;
    [SerializeField] ObjPlayerBullet bulletType;
    [SerializeField] private int[] upgradeCost;

    public string GetName() => turretName;
    public string GetDescription() => description;
    public Sprite GetSprite() => artworks;
    public int[] GetMinDamage() => minDamage;
    public int[] GetMaxDamage() => maxDamage;
    public int[] GetAmmoCount() => ammoCount;
    public float[] GetRateOfFire() => rateOfFire;
    public float[] GetReloadTime() => reloadTime;
    public float GetKnockBack() => knockBack;
    public float[] GetBulletSpeed() => bulletSpeed;
    public float GetLifetime() => lifetime;
    public ObjPlayerBullet GetBulletType() => bulletType;
    public int[] GetUpgradeCost() => upgradeCost;
}
