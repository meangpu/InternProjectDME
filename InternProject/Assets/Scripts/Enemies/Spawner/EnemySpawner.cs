using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] WaveManager waveManager;
    [SerializeField] private float waitTime;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<EnemyObj> enemys = new List<EnemyObj>();

    [Header("Pool")]
    [SerializeField] private Pooler enemyPool;


    void Start()
    {
        waveManager.SetMaxEC();
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        EnemyObj nowEne = enemys[Random.Range (0, enemys.Count)];
        // Debug.Log(nowEne);
        if (waveManager.EC_Point - nowEne.GetEC() >= 0)
        {
            // Instantiate(nowEne.enemy, transform.position,  Quaternion.Euler(new Vector3(0, 0, 90)));
            GameObject g = enemyPool.GetObject();
            g.GetComponent<EnemyDisplay>().StartDisplay(nowEne);  // set enemy to scriptable obj
            g.transform.position = transform.position;
            g.transform.rotation = transform.rotation;
            g.SetActive(true);

            g.GetComponent<EnemyFollow>().setupTrack();

            waveManager.EC_Point -= nowEne.GetEC();
            waveManager.SetECSlider();
        }
        else
        {
            // Debug.Log("No EC point left");
        }

        yield return new WaitForSeconds(waitTime);
        StartCoroutine(SpawnEnemy());
    }

}
