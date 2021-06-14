using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialSpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemPfb;
    [SerializeField] Transform[] tranList;

    public void SpawnEnem()
    {
        foreach (var point in tranList)
        {
            Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); 
            GameObject _enem = Instantiate(enemPfb, point.position+(randomVector*0.2f), Quaternion.identity);
            _enem.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.gameObject.tag == "PlayerBullet")
        {
            SpawnEnem();
            PoolingSingleton.Instance.PlayerBulletPool.ReturnObject(other.gameObject);
        }
    }

    
}
