using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerImage;
    [SerializeField] private SpriteRenderer turretImage;
    [SerializeField] private Player playerScpt;
    private Tank tank; 
    private TankTurret turret; 

    void Start()
    {
        turret = playerScpt.GetTurret();
        tank = playerScpt.GetTank();
        
        playerImage.sprite = tank.GetSprite();
        turretImage.sprite = turret.GetSprite();
    }


}
