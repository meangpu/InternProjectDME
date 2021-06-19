using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Tank Customization", menuName = "Saves/Create Tank Customization Data")]
public class PlayerTankCustomization : ScriptableObject
{   
    [SerializeField] private ObjPlayerTank tank;
    [SerializeField] private ObjTankTurret turret;


    private void OnEnable()
    {
        ////// prevent data reset across scene
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void SetTurret(ObjTankTurret turret)
    {
        this.turret = turret;
    }

    public void SetTank(ObjPlayerTank tank)
    {
        this.tank = tank;
    }

    public ObjTankTurret GetTurret() => turret;
    public ObjPlayerTank GetTank() => tank;
}
