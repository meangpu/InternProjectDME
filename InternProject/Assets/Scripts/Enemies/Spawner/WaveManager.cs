using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public int EC_Point;
    public Slider sliderEneSpawner;

    public void decreasePoint(int reduce)
    {
        EC_Point -= reduce;
    }

    public void addPoint(int addValue)
    {
        EC_Point += addValue;
    }

    public void SetECSlider()
    {
        sliderEneSpawner.value = EC_Point;
    }

    public void SetMaxEC()
    {
        sliderEneSpawner.maxValue = EC_Point;
        sliderEneSpawner.value = EC_Point;
    }

}
