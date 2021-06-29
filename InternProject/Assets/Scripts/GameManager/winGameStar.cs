using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class winGameStar : MonoBehaviour
{
    [SerializeField] GameObject fillChild;

    public void getThisStar()
    {
        fillChild.SetActive(true);
    }

    public void disThisStar()
    {
        fillChild.SetActive(false);
    }
}
