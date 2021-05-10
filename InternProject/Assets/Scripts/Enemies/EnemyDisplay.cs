using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    public Enemy enemy;
    public SpriteRenderer eneImage;
    private float eneSpeed;

    void Start()
    {
        eneImage.sprite = enemy.artWork;
        eneSpeed = enemy.moveSpeed;
    }

}
