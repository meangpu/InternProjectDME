using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setArrowToQuestPosition : MonoBehaviour
{
    [SerializeField] Transform spawnerTransform;
    Vector3 targetPosition;
    
    [SerializeField] float borderSize = 100f;
    [Header("transform")]
    [SerializeField] RectTransform pointerTransform;
    [SerializeField] RectTransform enemyInfo;
    [SerializeField] RectTransform clickCallWave;

    [Header("Cam")]
    [SerializeField] Camera UICam;
    [SerializeField] Camera mainCam;

    [Header("Image")]
    [SerializeField] Sprite arrowSprite;
    [SerializeField] Sprite circleSprite;

    [SerializeField] Image imageSelf;

    private GameManager gameManager;


   
    private void Awake() 
    {
        targetPosition = spawnerTransform.position;
    }

    private void Start() 
    {
        gameManager = GameManager.Instance;
		gameManager.OnBuyModeTrigger += HandleBuyModeTrigger;
    }

	private void OnDestroy() 
	{
		gameManager.OnBuyModeTrigger -= HandleBuyModeTrigger;
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
            imageSelf.sprite = arrowSprite;
            Vector3 capTarScreenPos = targetPosScreenPoint;
            capTarScreenPos.x = Mathf.Clamp(capTarScreenPos.x, borderSize, Screen.width - borderSize);
            capTarScreenPos.y = Mathf.Clamp(capTarScreenPos.y, borderSize, Screen.height - borderSize);

            Vector3 pointerWorldPos = UICam.ScreenToWorldPoint(capTarScreenPos);

            setAllPos(pointerWorldPos);
        }
        else
        {
            imageSelf.sprite = circleSprite;
            pointerTransform.localEulerAngles = new Vector3(0, 0, 0);
            Vector3 pointerWorldPos = mainCam.ScreenToWorldPoint(targetPosScreenPoint);
            setAllPos(pointerWorldPos);
        }
    }

    void setAllPos(Vector3 _pos)
    {
        pointerTransform.position = _pos;
        pointerTransform.localPosition = new Vector3(pointerTransform.localPosition.x, pointerTransform.localPosition.y, 0f);

        enemyInfo.position = _pos;
        enemyInfo.localPosition = new Vector3(enemyInfo.localPosition.x+190f, enemyInfo.localPosition.y+163.55f, 0f);

        clickCallWave.position = _pos;
        clickCallWave.localPosition = new Vector3(clickCallWave.localPosition.x-250f, clickCallWave.localPosition.y-5f, 0f);
    }


    void HandleBuyModeTrigger(bool state)
    {
        switch (state)
        {
            case true:
                // disable in play mode
                transform.GetChild(0).gameObject.SetActive(false);
                return;
            case false:

                if ( transform.GetChild(0).GetChild(1).childCount > 1)  // check if it have info or not
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                return;
        }
    }

}
