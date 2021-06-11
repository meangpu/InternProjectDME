using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    private static WaveManager instance;

    public static WaveManager Instance 
    { 
        get 
        { return instance; }
    }

    [Header("UIthing")]
    public Slider minWaveSlider;
    public Slider bigWaveSlider;
    public TMP_Text enemyLefttext;
    [SerializeField] private GameManager gameManager;

    [Header("WaveInfo")]
    [SerializeField] float timeBeforeWinPanel;
    [SerializeField] bool useRandom;
    [SerializeField] float timeBeforeNextWave;
    private float countDown;
    public static List<Enemy> EnemyAlive = new List<Enemy>();
    public EnemyWave[] EnemyWaves;
    private int waveindex = 0;
    private PoolingSingleton pooler;
    int thisWaveCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        // need to clear enemy data to prevent game not start on second time
        EnemyAlive.Clear();
        SetUp_MaxSlider(EnemyWaves.Length);
        countDown = 1;

        pooler = PoolingSingleton.Instance;
    }

    public void Set_MinSlider(int _value)
    {
        minWaveSlider.value = _value;
    }

    public void SetUp_MinSlider(int _value)
    {
        minWaveSlider.maxValue = _value;
        minWaveSlider.value = _value;
    }

    public void Set_MaxSlider(int _value)
    {
        bigWaveSlider.value = _value;
    }

    public void SetUp_MaxSlider(int _value)
    {
        bigWaveSlider.maxValue = _value;
        bigWaveSlider.value = _value;
    }

    public void SetEnemyLeftText()
    {
        enemyLefttext.text = EnemyAlive.Count.ToString();
    }

    private void Update() 
    {

        if (EnemyAlive.Count > 0)
        {
            return;
        }

	    if (waveindex == EnemyWaves.Length)  // when it going to go outside index range 
		{
            // happen when enemy reach 0 and there no next wave
			enabled = false;
            StartCoroutine(gameManager.LevelWon(timeBeforeWinPanel));
		}

		if (countDown <= 0f)
		{
            if (waveindex < EnemyWaves.Length)
            {
                StartCoroutine(SpawnWave());
                countDown = timeBeforeNextWave;
                return;
            }

		}

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        
    }

    private int CountAllEnemyInWave()
    {
        int _thisWaveCount = 0;
        EnemyWave wave = EnemyWaves[waveindex];
        foreach (var pointToSpawn in wave.EnemyAndPoint) 
        {
            foreach (var enemy in pointToSpawn.EnemyList)
            {
                _thisWaveCount += enemy.count;
            }
        }

        thisWaveCount = _thisWaveCount;
        return _thisWaveCount;
    }

    private IEnumerator SpawnWave()
    {
        EnemyWave wave = EnemyWaves[waveindex];
        Set_MaxSlider(EnemyWaves.Length - (waveindex+1));
        SetUp_MinSlider(CountAllEnemyInWave());

        foreach (var pointToSpawn in wave.EnemyAndPoint)  // loop through all spawn point
        {
            foreach (var enemy in pointToSpawn.EnemyList)
            {
                
                for (int i = 0; i < enemy.count; i++)
                {
                    if (enemy.isBoss)
                    {
                        SpawnBoss(enemy.enemyPfb, pointToSpawn.spawnPoint, enemy.enemy);
                        yield return new WaitForSeconds(wave.spawnRate);
                    }
                    else
                    {
                        SpawnEnemy(enemy.enemy, pointToSpawn.spawnPoint);
                        yield return new WaitForSeconds(wave.spawnRate);
                    }
                    thisWaveCount--;
                    Set_MinSlider(thisWaveCount);

                }
            }
        } 
        waveindex++;

    }

    private void SpawnEnemy(ObjEnemy enemy, Transform spawnPos)
    {
        if (enemy.GetEnemyType() != EnemyType.Machine) 
        { 
            pooler.EnemyPool.SpawnEnemy(spawnPos.position, spawnPos.rotation, enemy);
        }
        else
        {
            switch (enemy.GetEnemyId())
            {
                default:
                    break;
                case EnemyId.TolusinTank:
                    pooler.EnemyBasicTankPool.SpawnEnemy(spawnPos.position, spawnPos.rotation, enemy);
                    break;
                case EnemyId.ArtilleryTank:
                    pooler.EnemyArtilleryTankPool.SpawnEnemy(spawnPos.position, spawnPos.rotation, enemy);
                    break;
                case EnemyId.DoomsdayTank:
                    pooler.EnemyDoomsdayTankPool.SpawnEnemy(spawnPos.position, spawnPos.rotation, enemy);
                    break;
                case EnemyId.SpyPlane:
                    pooler.EnemySpyPlanePool.SpawnEnemy(spawnPos.position, spawnPos.rotation, enemy);
                    break;
                case EnemyId.BomberPlane:
                    pooler.EnemyBomberPlanePool.SpawnEnemy(spawnPos.position, spawnPos.rotation, enemy);
                    break;
                case EnemyId.MWing:
                    break;
            }    
        }
  
        SetEnemyLeftText();
    }

    private void SpawnBoss(GameObject bossPfb, Transform spawnPos, ObjEnemy enemy)
    {
        GameObject boss = Instantiate(bossPfb, spawnPos);
        

        boss.GetComponent<EnemyDisplay>().StartDisplay(enemy);  // set enemy to scriptable obj
        boss.transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);

        boss.GetComponent<EnemyShoot>().StartShooting();
        EnemyAlive.Add(boss.GetComponent<Enemy>());
        SetEnemyLeftText();
    }

    public List<Enemy> EnemyList => EnemyAlive;

}
