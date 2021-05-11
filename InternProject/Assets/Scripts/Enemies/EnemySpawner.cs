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
    [SerializeField] int EC_Point;
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
        if (EC_Point - nowEne.EC >= 0)
        {
            Instantiate(nowEne.enemy, transform.position,  Quaternion.Euler(new Vector3(0, 0, 90)));
            EC_Point -= nowEne.EC;
            SetEC(EC_Point, EC_Point + nowEne.EC);
        }
        else
        {
            Debug.Log("No EC point left");
        }

        yield return new WaitForSeconds(waitTime);
        StartCoroutine(spawnEnemy());
    }

    public void SetEC(int point, int oldPoint)
    {
        siderEneSpawner.value = point;
    }

    public void SetMaxEC()
    {
        siderEneSpawner.maxValue = EC_Point;
        siderEneSpawner.value = EC_Point;
    }



}
