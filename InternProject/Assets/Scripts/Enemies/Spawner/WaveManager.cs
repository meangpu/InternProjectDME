using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("UIthing")]
    public Slider minWaveSlider;
    public Slider bigWaveSlider;
    public TMP_Text enemyLefttext;
    [SerializeField] private GameManager gameManager;

    [Header("Pool")]
    [SerializeField] private Pooler enemyPool;

    [Header("WaveInfo")]
    [SerializeField] bool useRandom;
    [SerializeField] float timeBeforeNextWave;
    private float countDown;
    public static int EnemyAlive = 0;
    public EnemyWave[] EnemyWaves;
    private int waveindex = 0;



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
        enemyLefttext.text = EnemyAlive.ToString();
    }


    private void Start() 
    {
        SetUp_MaxSlider(EnemyWaves.Length);
        countDown = 0;
    }


    private void Update() 
    {
        if (EnemyAlive > 0)
        {
            return;
        }

	    if (waveindex == EnemyWaves.Length)  // when it going to go outside index range 
		{
            // happen when enemy reach 0 and there no next wave
			this.enabled = false;
            gameManager.LevelWon();
            Debug.Log("YOU WIN!!");
		}

		if (countDown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countDown = timeBeforeNextWave;
			return;
		}

        // if (EnemyWaves[waveindex].EC > 0)
        // {

        // }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        
    }



    IEnumerator SpawnWave()
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
                        
                        if (wave.EC - enemy.enemy.GetEC() >= 0)
                        {
                            SpawnEnemy(enemy.enemy, pointToSpawn.spawnPoint);
                            wave.EC -= enemy.enemy.GetEC();
                            Set_MinSlider(wave.EC);
                            yield return new WaitForSeconds(wave.spawnRate);
                        }
                    }
                }
                //////////////////// -- STABLE --/////////////////////////////
            }
            
   
        } 
        waveindex++;

    }


    void SpawnEnemy(EnemyObj enemy, Transform spawnPos)
    {
        GameObject g = enemyPool.GetObject();
        g.GetComponent<EnemyDisplay>().StartDisplay(enemy);  // set enemy to scriptable obj
        g.transform.position = spawnPos.position;
        g.transform.rotation = spawnPos.rotation;
        g.SetActive(true);

        g.GetComponent<EnemyFollow>().SetupTrack();
        g.GetComponent<EnemyShoot>().StartShoot();
        
        // add enemy count 
        EnemyAlive++;  

        SetEnemyLeftText();
    }


}
