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

    [Header("Pool")]
    [SerializeField] private Pooler enemyPool;

    [Header("WaveInfo")]
    [SerializeField] float timeBeforeWinPanel;
    [SerializeField] bool useRandom;
    [SerializeField] float timeBeforeNextWave;
    private float countDown;
    public static List<EnemyGetHit> EnemyAlive = new List<EnemyGetHit>();
    public EnemyWave[] EnemyWaves;
    private int waveindex = 0;

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

    private IEnumerator SpawnWave()
    {
        EnemyWave wave = EnemyWaves[waveindex];
        SetUp_MinSlider(wave.EC);
        Set_MaxSlider(EnemyWaves.Length - (waveindex+1));

        foreach (var pointToSpawn in wave.EnemyAndPoint)  // loop through all spawn point
        {
            if (useRandom)
            {
                float rand = Random.value; // random number between 0 and 1
                while (wave.EC > 0)
                {
                    foreach (var enemy in pointToSpawn.EnemyList)
                    {
                        if(enemy.prob > rand) // if random number less than probobility
                        {
                            if (wave.EC - enemy.enemy.GetEC() >= 0)  // spawn when wave have enough ec
                            {
                                SpawnEnemy(enemy.enemy, pointToSpawn.spawnPoint);
                                wave.EC -= enemy.enemy.GetEC();

                                Set_MinSlider(wave.EC);
                                yield return new WaitForSeconds(wave.spawnRate);
                            }
                        }
                        else
                        {
                            rand = Random.value;
                        }
                    }

                }
            }
            else
            {
                //////////////////// -- STABLE --/////////////////////////////
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
                            if (wave.EC - enemy.enemy.GetEC() >= 0)
                            {
                                SpawnEnemy(enemy.enemy, pointToSpawn.spawnPoint);
                                wave.EC -= enemy.enemy.GetEC();
                                Set_MinSlider(wave.EC);
                                yield return new WaitForSeconds(wave.spawnRate);
                            }
                        }
                    }
                }
                //////////////////// -- STABLE --/////////////////////////////
            }
            
   
        } 
        waveindex++;

    }

    private void SpawnEnemy(ObjEnemy enemy, Transform spawnPos)
    {
        GameObject g = enemyPool.GetObject();
        g.GetComponent<EnemyDisplay>().StartDisplay(enemy);  // set enemy to scriptable obj
        g.transform.position = spawnPos.position;
        g.transform.rotation = spawnPos.rotation;
        g.SetActive(true);

        g.GetComponent<EnemyFollow>().SetupTrack();
        g.GetComponent<EnemyShoot>().StartShoot();
        
        // add enemy count 
        EnemyAlive.Add(g.GetComponent<EnemyGetHit>());  

        SetEnemyLeftText();
    }

    private void SpawnBoss(GameObject bossPfb, Transform spawnPos, ObjEnemy enemy)
    {
        GameObject boss = Instantiate(bossPfb, spawnPos);
        

        boss.GetComponent<EnemyDisplay>().StartDisplay(enemy);  // set enemy to scriptable obj
        boss.transform.position = spawnPos.position;
        boss.transform.rotation = spawnPos.rotation;

        boss.GetComponent<EnemyFollow>().SetupTrack();
        boss.GetComponent<EnemyShoot>().StartShoot();
        EnemyAlive.Add(boss.GetComponent<EnemyGetHit>());
        SetEnemyLeftText();
    }



    public List<EnemyGetHit> EnemyList => EnemyAlive;


}
