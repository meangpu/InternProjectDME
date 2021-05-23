using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerImage;
    [SerializeField] private Player playerScpt;
    private Tank tank; 

    void Start()
    {
        tank = playerScpt.GetTank();
        playerImage.sprite = tank.GetSprite();
    }


}
