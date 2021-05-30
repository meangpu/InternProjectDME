using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New player Bullet", menuName="ObjPlayerTank/Create New Bullet")]
public class ObjPlayerBullet : ScriptableObject
{
    [SerializeField] string bulletName;
    [SerializeField] float bulletSpeed;
    [SerializeField] Sprite artWork;
    [SerializeField] Material material;

    public string GetName() => bulletName;
    public float GetSpeed() => bulletSpeed;
    public Sprite GetArtWork() => artWork;
    public Material GetMaterial() => material;

}
