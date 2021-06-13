using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class callWaveEarly : MonoBehaviour
{
    [SerializeField] GameObject infoPfb;
    [SerializeField] Transform parentInfoTransform;
    [SerializeField] Button mainButton;
    [SerializeField] GameObject TextTellCallEarly;


    public void SetData(EnemyProbObj[] listOfEnemy)
    {
        mainButton.interactable = true;
        foreach (var enemy in listOfEnemy)
        {
            var nowInfo = Instantiate(infoPfb, parentInfoTransform);
            nowInfo.transform.SetParent(parentInfoTransform);
            nowInfo.GetComponent<InfoSetter>().ShowData(enemy.enemy, enemy.count);
            
        }
    }

    public IEnumerator ShowDataForSec(float _wait)
    {
        ShowDataNotMouse();
        yield return new WaitForSeconds(_wait);
        HideData();
    }

    public void ClearOldData()
    {
        for (int i = 0; i < parentInfoTransform.childCount; i++)
        {
            if (i == 0) continue;  // ignor text 
            Destroy(parentInfoTransform.GetChild(i).gameObject);
        }
        HideData();
    }

    public void HideData()
    {
        if (parentInfoTransform.childCount > 1)
        {
            mainButton.interactable = true;
        }
        else
        {
            mainButton.interactable = false;
        }
        TextTellCallEarly.SetActive(false);
        parentInfoTransform.gameObject.SetActive(false);
    }

    public void ShowDataMouse()
    {
        if (parentInfoTransform.childCount > 1)
        {
            mainButton.interactable = true;
            TextTellCallEarly.SetActive(true);
            parentInfoTransform.gameObject.SetActive(true);
        }
    }

    public void ShowDataNotMouse()
    {
        if (parentInfoTransform.childCount > 1)
        {
            mainButton.interactable = true;
            TextTellCallEarly.SetActive(false);
            parentInfoTransform.gameObject.SetActive(true);
        }
    }


}
