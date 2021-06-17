using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class starManager : MonoBehaviour
{
    [SerializeField] TMP_Text textStarValue;
    [SerializeField] ObjStarData starData;

    public static starManager Instance { get { return _instance; } }
    private static starManager _instance; 

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;  //Avoid doing anything else
        }
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start() 
    {
        textStarValue.text = starData.GetStar().ToString();
    }

    public int getNowStar()
    {
        return starData.GetStar();
    }






}
