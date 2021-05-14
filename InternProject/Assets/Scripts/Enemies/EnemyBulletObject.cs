using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletObject : ScriptableObject
{
    [SerializeField] private string bulletName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;

    public Sprite GetSprite() => sprite;
    public float GetSpeed() => bulletSpeed;
    public float GetLifeTime() => lifeTime;
}
