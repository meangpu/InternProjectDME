using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerImage = null;
    [SerializeField] private SpriteRenderer turretImage = null;
    [SerializeField] private Player playerScpt;
    private ObjPlayerTank tank; 
    private ObjTankTurret turret; 

    private void Start()
    {
        turret = playerScpt.GetTurret();
        tank = playerScpt.GetTank();

        playerImage.sprite = tank.GetSprite();
        turretImage.sprite = turret.GetSprite();
    }
}
