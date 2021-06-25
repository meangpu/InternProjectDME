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

    [Header("Bosses Prefab")]
    [SerializeField] private GameObject falconBoss = null;
    [SerializeField] private GameObject tankBoss = null;

    [Header("UIthing")]
    [SerializeField] TMP_Text textTimeBeforeNextWave;
    [SerializeField] TMP_Text waveRemaintext;
    [SerializeField] GameObject waveInfoObj;

    [SerializeField] private GameManager gameManager;
    [SerializeField] GameObject firstWaveText;

    [Header("WaveInfo")]
    [SerializeField] callWaveEarly[] spawnPointList;
    [SerializeField] float showInfoForSec;
    [SerializeField] float timeBeforeWinPanel;
    [SerializeField] Transform playerTrans;

    private float countDown;
    public static List<Enemy> EnemyAlive = new List<Enemy>();
    public EnemyWave[] EnemyWaves;
    private int waveindex = 0;
    private PoolingSingleton pooler;
    int thisWaveCount;

    bool isStopCount;

    bool isFirstWave = true;

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

        // first Time Count down
        countDown = 2;

        pooler = PoolingSingleton.Instance;
    }


    private void Update() 
    {
        if (isFirstWave & countDown!=0)
        {
            CheckNextWave(0, true);
            return;
        }

	    if (waveindex == EnemyWaves.Length)  // when it going to go outside index range 
		{
            if (EnemyAlive.Count > 0)
            {
                countDown = 0;
                textTimeBeforeNextWave.text = countDown.ToString("F0");

                CheckNextWave(0);
                return;
            }
            
            // happen when enemy reach 0 and there no next wave
			this.enabled = false;
            StartCoroutine(gameManager.LevelWon(timeBeforeWinPanel));
		}

		if (countDown <= 0f)
		{
            if (waveindex < EnemyWaves.Length)
            {
                ClearAllOldData();
                StartCoroutine(SpawnWave());
                countDown = EnemyWaves[waveindex].TimeBeforeNextWave;
                isFirstWave = false;
                firstWaveText.SetActive(false);

                return;
            }
		}

        if (isStopCount)
        {
            countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
            textTimeBeforeNextWave.text = countDown.ToString("F0");
            return;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        textTimeBeforeNextWave.text = countDown.ToString("F0");
        
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

    void updateWavetext()
    {
        if (waveindex+1 > EnemyWaves.Length)
        {
            return;
        }
        waveRemaintext.text = $"wave {waveindex+1}/{EnemyWaves.Length}";
        waveInfoObj.SetActive(true);
    }

    private IEnumerator SpawnWave()
    {
        isStopCount = true;
        EnemyWave wave = EnemyWaves[waveindex];
        CountAllEnemyInWave();
        updateWavetext();

        foreach (var pointToSpawn in wave.EnemyAndPoint)  // loop through all spawn point
        {
            foreach (var enemy in pointToSpawn.EnemyList)
            {
                pointToSpawn.spawnPoint.GetChild(0).GetChild(0).gameObject.SetActive(false);
                
                for (int i = 0; i < enemy.count; i++)
                {
                    if (i == enemy.count -1)  // last enemy
                    {
                        isStopCount = false;
                    }
                    SpawnEnemy(enemy.enemy, pointToSpawn.spawnPoint);
                    yield return new WaitForSeconds(wave.spawnRate);
                    
                    thisWaveCount--;
                    Debug.Log(thisWaveCount);
                    if (thisWaveCount <= 0)
                    {
                        
                        CheckNextWave();
                    }
                }
            }
        } 
        waveindex++;
        // updateWavetext();  // this is wrong 
    }

    public void callNextWave(Button buttonScpt)
    {

        updateWavetext();
        if (thisWaveCount > 0)
        {
            return;
        }
        int _tempData = (int)countDown;
        int goldGain = _tempData*2;  // get gold = 2*time remain

        countDown -= _tempData;
        textTimeBeforeNextWave.text = countDown.ToString("F0");
        buttonScpt.transform.parent.gameObject.SetActive(false);
        buttonScpt.interactable = false;

        if (isFirstWave)
        {
            return;
            // not add gold if first wave
        }

        PlayerStats.Instance.AddGold(goldGain);
        DamagePopup.Create(playerTrans.position, goldGain, DamagePopup.DamageType.Gold);
    }


    private void CheckNextWave(int aheadNum=1, bool firstWave=false)
    {
        if (waveindex+aheadNum >= EnemyWaves.Length)  // when it going to go outside index range 
		{
            ClearAllOldData();
            return;
        }

        ClearAllOldData();

        EnemyWave wave = EnemyWaves[waveindex+aheadNum];

        foreach (var pointToSpawn in wave.EnemyAndPoint)  // loop through all spawn point
        {
            pointToSpawn.spawnPoint.GetChild(0).GetComponent<callWaveEarly>().SetData(pointToSpawn.EnemyList);

            if (gameManager.isBuying)
            {
                pointToSpawn.spawnPoint.GetChild(0).GetChild(0).gameObject.SetActive(true);  // = canvas
            }          

            
            if (firstWave)
            {
                pointToSpawn.spawnPoint.GetChild(0).GetComponent<callWaveEarly>().ShowDataNotMouse();
            }
            else
            {
                StartCoroutine(pointToSpawn.spawnPoint.GetChild(0).GetComponent<callWaveEarly>().ShowDataForSec(showInfoForSec));
            }
            
        } 
    }

    private void ClearAllOldData()
    {
        foreach (var spawnPoint in spawnPointList)
        {
            spawnPoint.ClearOldData();
        }
    }


    private void SpawnEnemy(ObjEnemy enemy, Transform spawnPos)
    {
        switch (enemy.GetEnemyType())
        {
            default:
                pooler.EnemyPool.SpawnEnemy(spawnPos.position, spawnPos.rotation, enemy);
                break;

            case EnemyType.Machine:
            case EnemyType.Plane:
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
                        pooler.MWingPool.SpawnEnemy(spawnPos.position, spawnPos.rotation, enemy);
                        break;
                }
                break;

            case EnemyType.BossFalcon:
                SpawnBoss(falconBoss, spawnPos, enemy);
                break;

            case EnemyType.BossTank:
                SpawnBoss(tankBoss, spawnPos, enemy);
                break;
        }
  
    }

    private void SpawnBoss(GameObject bossPfb, Transform spawnPos, ObjEnemy enemy)
    {
        GameObject boss = Instantiate(bossPfb, spawnPos.position, Quaternion.identity);
        boss.GetComponent<EnemyDisplay>().StartDisplay(enemy);  // set enemy to scriptable obj
        boss.transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);

        boss.GetComponent<EnemyShoot>().StartShooting();
        EnemyAlive.Add(boss.GetComponent<Enemy>());
    }

    public List<Enemy> EnemyList => EnemyAlive;

}
