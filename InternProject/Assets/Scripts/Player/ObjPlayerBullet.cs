using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New player Bullet", menuName="ObjPlayerTank/Create New Bullet")]
public class ObjPlayerBullet : ScriptableObject
{
    [SerializeField] string bulletName;
    [SerializeField] Sprite artWork;
    [SerializeField] Material material;

    public string GetName() => bulletName;
    public Sprite GetArtWork() => artWork;
    public Material GetMaterial() => material;
}
