using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showStar : MonoBehaviour
{
    [SerializeField] TMP_Text textStarValue;
    [SerializeField] ObjStarData starData;

    private void Start() 
    {
        textStarValue.text = starData.GetStar().ToString();
    }


}
