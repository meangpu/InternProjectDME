using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyProbObj
    {
        public EnemyObj enemy;
        public float prob;  // 0 - 1 
    }

    [System.Serializable]
    public class EnemyAndPoint
    {
        public EnemyProbObj[] EnemyList;
        public Transform spawnPoint;
    }

    [System.Serializable]
    public class EnemyWave
    {
        public int EC;
        public float countDown;
        public int timeBeforeNextWave;
        public EnemyAndPoint[] EnemyAndPoint;
    }

    
    public int EC_Point;
    public Slider sliderEneSpawner;
    [Header("UIthing")]
    public TMP_Text enemyLefttext;
    [Header("WaveInfo")]
    public static int EnemyAlive = 0;
    public EnemyWave[] EnemyWaves;

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

    public void SetEnemyLeftText()
    {
        enemyLefttext.text = WaveManager.EnemyAlive.ToString();
    }

    [ContextMenu("dasdw")]
    public void meangpu()
    {
        Debug.Log(EnemyAlive);
    }

}
