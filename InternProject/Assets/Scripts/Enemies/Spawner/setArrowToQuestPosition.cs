using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setArrowToQuestPosition : MonoBehaviour
{
    [SerializeField] Transform spawnerTransform;
    Vector3 targetPosition;
    
    [SerializeField] float borderSize = 100f;
    [SerializeField] RectTransform pointerTransform;
    [SerializeField] Camera UICam;
    [SerializeField] Camera mainCam;
   
    private void Awake() 
    {
        targetPosition = spawnerTransform.position;
    }


    private void Update() 
    {
        Vector3 toPos = targetPosition;
        Vector3 fromPos = mainCam.transform.position;
        fromPos.z = 0f;
        Vector3 dir = (toPos-fromPos).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        pointerTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPosScreenPoint = mainCam.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPosScreenPoint.x <=borderSize || targetPosScreenPoint.x >=Screen.width - borderSize || targetPosScreenPoint.y <= borderSize || targetPosScreenPoint.y >= Screen.height - borderSize; 

        if (isOffScreen)
        {
            Vector3 capTarScreenPos = targetPosScreenPoint;
            capTarScreenPos.x = Mathf.Clamp(capTarScreenPos.x, borderSize, Screen.width - borderSize);
            capTarScreenPos.y = Mathf.Clamp(capTarScreenPos.y, borderSize, Screen.height - borderSize);

            Vector3 pointerWorldPos = UICam.ScreenToWorldPoint(capTarScreenPos);
            pointerTransform.position = pointerWorldPos;
            pointerTransform.localPosition = new Vector3(pointerTransform.localPosition.x, pointerTransform.localPosition.y, 0f);
        }
        else
        {
            Vector3 pointerWorldPos = UICam.ScreenToWorldPoint(targetPosScreenPoint);
            pointerTransform.position = pointerWorldPos;
            pointerTransform.localPosition = new Vector3(pointerTransform.localPosition.x, pointerTransform.localPosition.y, 0f);
        }
    }

}
