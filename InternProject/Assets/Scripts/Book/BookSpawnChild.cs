using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawnChild : MonoBehaviour
{
    [SerializeField] ObjEnemy[] enemyLists;
    [SerializeField] GameObject EnemyImagePrefabs;

    void Start()
    {
        foreach (var enemy in enemyLists)
        {
            GameObject newData = Instantiate(EnemyImagePrefabs, gameObject.transform);
            newData.GetComponent<BookChildSetup>().showData(enemy); 
            
        }
    }


}
