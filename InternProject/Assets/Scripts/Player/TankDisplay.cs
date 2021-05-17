using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TankDisplay : MonoBehaviour
{
    public Tank tank;
    public TMP_Text textName;
    public SpriteRenderer tankImage;
    public float tankSpeed;

    void Start()
    {
        textName.text = tank.GetName();
        tankImage.sprite = tank.GetSprite();
    }
}
