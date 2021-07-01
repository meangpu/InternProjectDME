using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamToBuyBox : MonoBehaviour
{
    [SerializeField] Transform camBuyMode;
    [SerializeField] Transform camTarget;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            camBuyMode.position = camTarget.position;
        }
    }

}
