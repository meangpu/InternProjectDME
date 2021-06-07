using UnityEngine;

[System.Serializable]
public class EnemyProbObj
{
    public ObjEnemy enemy;
    public GameObject enemyPfb;
    public bool isBoss;
    public int count;
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
    public float spawnRate;
    public EnemyAndPoint[] EnemyAndPoint;
}


