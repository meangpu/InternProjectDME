using UnityEngine;

[System.Serializable]
public class EnemyProbObj
{
    public ObjEnemy enemy;
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
    [Header("Wave Constant")]
    [Tooltip("Spawn time in second 1 mean spawn 1 every 1 seconds")]
    public float spawnRate;
    public float TimeBeforeNextWave;
    [Space]
    [Space]
    public EnemyAndPoint[] EnemyAndPoint;
}


