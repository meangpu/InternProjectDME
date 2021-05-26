using UnityEngine;

[System.Serializable]
public class EnemyProbObj
{
    public ObjEnemy enemy;
    public int count;
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
    public float spawnRate;
    public EnemyAndPoint[] EnemyAndPoint;
}


