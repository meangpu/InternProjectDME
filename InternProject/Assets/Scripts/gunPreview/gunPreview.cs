using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gunPreview : MonoBehaviour
{
    [SerializeField] ObjTankTurret turret;
    [SerializeField] TMP_Text gunName;
    [SerializeField] SpriteRenderer gunImg;

    void Start()
    {
        gunName.text = turret.GetName();
        gunImg.sprite = turret.GetSprite();
    }


}
