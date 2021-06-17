using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="StarData", menuName="Data/Create star data")]
public class ObjStarData : ScriptableObject
{
    [SerializeField] int starValue;


    public int GetStar() => starValue;

    public void addValue(int _value)
    {
        starValue += _value;
    }

    public void subtractValue(int _value)
    {
        if(checkCanSubtract(_value))
        {
            starValue -= _value;
        }
        else
        {
            Debug.Log("cannot subtract");
        }
    }

    bool checkCanSubtract(int _value)
    {
        if (starValue - _value >= 0)
        {
            return true;
        }
        return false;
    }


}
