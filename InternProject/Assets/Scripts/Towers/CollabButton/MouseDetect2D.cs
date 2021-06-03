using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDetect2D : MonoBehaviour 
{
	[Header ("Camera")]
	[SerializeField] Camera mainCam;
	
    private void Update() 
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit)
        {
            Debug.Log(hit);
        }
    }
}
