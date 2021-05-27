using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSetter : MonoBehaviour
{
    private int damage;
    private float knockBack;

    public int Damage { get { return damage; } set { damage = value; } }
    public float KnockBack { get { return knockBack; } set { knockBack = value; } }
}
