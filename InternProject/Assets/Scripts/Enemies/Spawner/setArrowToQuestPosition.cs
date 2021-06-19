using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setArrowToQuestPosition : MonoBehaviour
{
    Vector3 targetPosition;
    [SerializeField] float borderSize = 100f;
    [SerializeField] RectTransform pointerTransform;
    [SerializeField] Camera UICam;
    [SerializeField] Camera mainCam;
   
    private void Awake() 
    {
        targetPosition = new Vector3(200, 45);
    }

    private void Update() 
    {
        Vector3 toPos = targetPosition;
        Vector3 fromPos = mainCam.transform.position;
        fromPos.z = 0f;
        Vector3 dir = (toPos-fromPos).normalized;
        float angle = Vector3.Angle(dir, transform.forward);
        pointerTransform.localEulerAngles = new Vector3(0, 0, angle);

        

        Vector3 targetPosScreenPoint = mainCam.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPosScreenPoint.x <=borderSize || targetPosScreenPoint.x >=Screen.width - borderSize || targetPosScreenPoint.y <= borderSize || targetPosScreenPoint.y >= Screen.height - borderSize; 

        if (isOffScreen)
        {
            Vector3 capTarScreenPos = targetPosScreenPoint;
            if (capTarScreenPos.x <= borderSize) capTarScreenPos.x = borderSize;
            if (capTarScreenPos.x >= Screen.width - borderSize) capTarScreenPos.x = Screen.width - borderSize;
            if (capTarScreenPos.y <= borderSize) capTarScreenPos.y = borderSize;
            if (capTarScreenPos.y >= Screen.height - borderSize) capTarScreenPos.y = Screen.height - borderSize;
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


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Vector3 targetPos)
    {
        gameObject.SetActive(true);
    }

}
