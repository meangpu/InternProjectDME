using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int EC_Point;

    public void decreasePoint(int reduce)
    {
        EC_Point -= reduce;
    }

    public void addPoint(int addValue)
    {
        EC_Point += addValue;
    }

}
