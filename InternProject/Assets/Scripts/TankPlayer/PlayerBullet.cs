using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Tank/Create New Bullet")]
public class PlayerBullet : ScriptableObject
{
    [SerializeField] private string bulletName;
    [SerializeField] private Sprite artWork;
}