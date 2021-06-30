using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpeedUp : MonoBehaviour
{
    public void SpeedUp()
    {
        Time.timeScale = 20.0f;
    }

    public void SpeedDefault()
    {
        Time.timeScale = 1.0f;
    }
}
