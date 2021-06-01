using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSetter : MonoBehaviour
{
    private int damage;
    private float knockBack;
    private float lifetime;
    private float bulletSpeed;

    public int Damage { get { return damage; } set { damage = value; } }
    public float KnockBack { get { return knockBack; } set { knockBack = value; } }
    public float Lifetime { get { return lifetime; } set { lifetime = value; } }
    public float BulletSpeed { get { return knockBack; } set { knockBack = value; } }
}
