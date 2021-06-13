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
        parentInfoTransform.gameObject.SetActive(true);
        yield return new WaitForSeconds(_wait);
        parentInfoTransform.gameObject.SetActive(false);
  
    }

    public void ClearOldData()
    {
        for (int i = 0; i < parentInfoTransform.childCount; i++)
        {
            if (i == 0) continue;  // ignor text 
            Destroy(parentInfoTransform.GetChild(i).gameObject);
        }
    }

    public void HideData()
    {
        mainButton.interactable = false;
        parentInfoTransform.gameObject.SetActive(false);
    }

}
