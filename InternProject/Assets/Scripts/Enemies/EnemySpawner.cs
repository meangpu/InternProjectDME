using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{

    [System.Serializable]
    public class EnemyList
    {
        public GameObject enemy;
        public int EC;
    }

    [Header("Info")]
    [SerializeField] WaveManager waveManager;
    [SerializeField] private float waitTime;
    [SerializeField] List<EnemyList> enemys = new List<EnemyList>();

    [Header("HP")]
    public Slider siderEneSpawner;


    void Start()
    {
        SetMaxEC();
        StartCoroutine(spawnEnemy());
    }

    private IEnumerator spawnEnemy()
    {
        EnemyList nowEne = enemys[Random.Range (0, enemys.Count)];
        Debug.Log(nowEne);
        if (waveManager.EC_Point - nowEne.EC >= 0)
        {
            Instantiate(nowEne.enemy, transform.position,  Quaternion.Euler(new Vector3(0, 0, 90)));
            waveManager.EC_Point -= nowEne.EC;
            SetECSlider(waveManager.EC_Point);
        }
        else
        {
            Debug.Log("No EC point left");
        }

        yield return new WaitForSeconds(waitTime);
        StartCoroutine(spawnEnemy());
    }

    public void SetECSlider(int point)
    {
        siderEneSpawner.value = point;
    }

    public void SetMaxEC()
    {
        siderEneSpawner.maxValue = waveManager.EC_Point;
        siderEneSpawner.value = waveManager.EC_Point;
    }



}
