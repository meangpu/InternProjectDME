using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    public SpriteRenderer eneImage;
    private float eneSpeed;

    public void StartDisplay(EnemyObj enemy)
    {
        eneImage.sprite = enemy.artWork;
        eneSpeed = enemy.moveSpeed;
    }

}
