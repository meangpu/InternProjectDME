using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerImage;
    [SerializeField] private SpriteRenderer turretImage;
    [SerializeField] private Player playerScpt;
    private ObjPlayerTank ObjPlayerTank; 
    private ObjTankTurret turret; 

    void Start()
    {
        turret = playerScpt.GetTurret();
        ObjPlayerTank = playerScpt.GetTank();
        
        playerImage.sprite = ObjPlayerTank.GetSprite();
        turretImage.sprite = turret.GetSprite();
    }


}
