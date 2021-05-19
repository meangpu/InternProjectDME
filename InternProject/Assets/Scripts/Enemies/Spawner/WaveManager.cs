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
    [SerializeField] float timeBeforeNextWave;
    private float countDown;
    public static int EnemyAlive = 0;
    public EnemyWave[] EnemyWaves;
    private int waveindex = 0;


    public void set_MinSlider(int _value)
    {
        minWaveSlider.value = _value;
    }

    public void setUp_MinSlider(int _value)
    {
        minWaveSlider.maxValue = _value;
        minWaveSlider.value = _value;
    }

    public void set_MaxSlider(int _value)
    {
        bigWaveSlider.value = _value;
    }

    public void setUp_MaxSlider(int _value)
    {
        bigWaveSlider.maxValue = _value;
        bigWaveSlider.value = _value;
    }


    public void SetEnemyLeftText()
    {
        enemyLefttext.text = WaveManager.EnemyAlive.ToString();
    }


    private void Start() 
    {
        setUp_MaxSlider(EnemyWaves.Length);
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

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        
    }



    IEnumerator SpawnWave()
    {
        EnemyWave wave = EnemyWaves[waveindex];
        setUp_MinSlider(wave.EC);
        set_MaxSlider(EnemyWaves.Length - (waveindex+1));

        foreach (var pointToSpawn in wave.EnemyAndPoint)  // loop through all spawn point
        {

            // float rand = Random.value; // random number between 0 and 1
            // Debug.Log(rand);

            foreach (var enemy in pointToSpawn.EnemyList)
            {
                for (int i = 0; i < enemy.count; i++)
                {
                    if (wave.EC - enemy.enemy.GetEC() >= 0)
                    {
                        SpawnEnemy(enemy.enemy, pointToSpawn.spawnPoint);
                        wave.EC -= enemy.enemy.GetEC();
                        set_MinSlider(wave.EC);
                        yield return new WaitForSeconds(wave.spawnRate);
                    }
                }


                // if(enemy.prob > rand) // if random number less than probobility
                // {
                //     if (wave.EC - enemy.enemy.GetEC() >= 0)  // spawn when wave have enough ec
                //     {
                //         SpawnEnemy(enemy.enemy, pointToSpawn.spawnPoint);
                //         wave.EC -= enemy.enemy.GetEC();

                //         set_MinSlider(wave.EC);
                //         yield return new WaitForSeconds(wave.spawnRate);
                //     }
                // }


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

        g.GetComponent<EnemyFollow>().setupTrack();
        g.GetComponent<EnemyShoot>().StartShoot();
        
        // add enemy count 
        EnemyAlive++;  

        SetEnemyLeftText();
    }


}
