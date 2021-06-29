using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class starSetter : MonoBehaviour
{
    [SerializeField] winGameStar star1;
    [SerializeField] winGameStar star2;
    [SerializeField] winGameStar star3;

    public void getStar(int _star)
    {
        if (_star == 0)
        {
            star1.disThisStar();
            star2.disThisStar();
            star3.disThisStar();
        }
        else if (_star == 1)
        {
            star1.getThisStar();
            star2.disThisStar();
            star3.disThisStar();
        }
        else if(_star == 2)
        {
            star1.getThisStar();
            star2.getThisStar();
            star3.disThisStar();
        }
        else if(_star == 3)
        {
            star1.getThisStar();
            star2.getThisStar();
            star3.getThisStar();
        }
        else
        {
            return;
        }
    }
}
