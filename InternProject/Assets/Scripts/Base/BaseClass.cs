using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewBase", menuName="NewBase")]
public class BaseClass : ScriptableObject
{
    public string tankName;
    public string tankDes;
    public Sprite artWork;
    public int hp;
    public int damage;
}
