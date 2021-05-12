using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    public EnemyObj enemy;
    public SpriteRenderer eneImage;
    private float eneSpeed;

    void Start()
    {
        eneImage.sprite = enemy.artWork;
        eneSpeed = enemy.moveSpeed;
    }

}
