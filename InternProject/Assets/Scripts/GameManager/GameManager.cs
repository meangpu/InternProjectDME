using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] CinemachineVirtualCamera buyModeCam;
    public bool isBuying;
    [SerializeField] private float respawnTime = 15f;
    [SerializeField] private float deathUITime = 3f;
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject pauseAllButton;

    [Header("GameOver")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    [Header("CameraControl")]
    [SerializeField] float zoomInMax;
    [SerializeField] float zoomOutMax;
    [SerializeField] camTarget camTarScript;
    [SerializeField]float NowZoomValue = 15;

    [Header("Victory")]
    [SerializeField] starSetter starScpt;
    [SerializeField] BaseClass baseScpt;

    // GameObject references
    private Player player;
    private BaseClass playerBase;
    private UIManager uiManager;

    public event Action<bool> OnBuyModeTrigger;
    public event Action OnCheckWhatCanBuy;

    private float respawnTimeRemaining = 0f;
    private float deathUITimeRemaining = 0f;
    private bool isPaused = false;
    private bool isDeathUIActive = false;

    private PlayerControls playerControls;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerBase = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseClass>();

        playerControls = new PlayerControls();
    }

    private void Start()
    {
        uiManager = UIManager.Instance;

        DisableZoom();
        ResumeGame();

        playerControls.Menu.Pause.performed += _ => HandlePause();
    }

    private void Update()
    {
        CheckZoom();

        if (respawnTimeRemaining == 0f) { return; }

        float deltaTime = Time.deltaTime;

        respawnTimeRemaining = Mathf.Max(respawnTimeRemaining - deltaTime, 0f);
        deathUITimeRemaining = Mathf.Max(deathUITimeRemaining - deltaTime, 0f);
        uiManager.UpdateRespawnBar(respawnTimeRemaining / respawnTime);

        HandleDeathUI();

        if (respawnTimeRemaining == 0f)
        {
            player.gameObject.SetActive(true);
            uiManager.ResetRespawnBar();
            PlayerStats.Instance.RespawnPlayer();
        }
    }

    private void HandleDeathUI()
    {
        if (deathUITimeRemaining != 0f || !isDeathUIActive) { return; }

        uiManager.HideDeathPanel();
        isDeathUIActive = false;
    }

    public void CheckZoom()
    {
        float direction = playerControls.BuyMenu.Zoom.ReadValue<float>();
        if (direction > 0)
        {    
            if (buyModeCam.m_Lens.OrthographicSize > zoomInMax)
            {
                NowZoomValue = buyModeCam.m_Lens.OrthographicSize - 2f;
            }
        }
        else if (direction < 0)
        {      
            if (buyModeCam.m_Lens.OrthographicSize < zoomOutMax)
            {
                NowZoomValue = buyModeCam.m_Lens.OrthographicSize + 2f;
            }  
        }

        NowZoomValue = Mathf.Clamp(NowZoomValue, zoomInMax, zoomOutMax);
        buyModeCam.m_Lens.OrthographicSize = Mathf.Lerp(buyModeCam.m_Lens.OrthographicSize, NowZoomValue, Time.deltaTime *15f);
    }

    public void GameOver()
    {
        StopGame();
        gameOverPanel.SetActive(true);
    }

    private void HandlePause()
    {
        switch (isPaused)
        {
            case true:
                ResumeGame();
                return;
            case false:
                PauseGame();
                return;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseAllButton.SetActive(true);
        isPaused = true;
        pausePanel.SetActive(true);
        DisableAllControls();
    }

    private void StopGame()
    {
        // no open ui option
        Time.timeScale = 0;
        isPaused = true;
        DisableAllControls();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        EnableAllControls();
    }

    public void restartLevel()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public IEnumerator LevelWon(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);

        starScpt.getStar(CheckStar());

        winPanel.SetActive(true);
        StopGame();
    }

    private int CheckStar()
    {
        float basePercent = baseScpt.getPercentHp();
        if (basePercent >= 90f)
        {
            return 3;
        }
        else if (basePercent >= 50f)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public void CheckWhatCanBuy()
    {
        OnCheckWhatCanBuy?.Invoke();
    }

    public void BuyModeSwap()
    {
        OnBuyModeTrigger?.Invoke(isBuying);

        if (isBuying)
        {
            DisableZoom();
            isBuying = false;
            buyModeCam.m_Priority = 0;
            uiManager.CloseBuyMenu();
        }
        else
        {
            EnableZoom();
            buyModeCam.m_Priority = 50;
            isBuying = true;
            uiManager.OpenBuyMenu();
        }
    }

    public void HandlePlayerDeath()
    {
        respawnTimeRemaining = respawnTime;
        deathUITimeRemaining = deathUITime;

        if (!isBuying) 
        { 
            BuyModeSwap(); 
        }
        
        uiManager.HandleDeathUI();
        isDeathUIActive = true;
        player.gameObject.SetActive(false);
    }

    private void DisableZoom()
    {
        playerControls.BuyMenu.Zoom.Disable();
    }

    private void EnableZoom()
    {
        playerControls.BuyMenu.Zoom.Enable();
    }

    private void EnableAllControls()
    {
        playerControls.Tank.Enable();
        playerControls.BuyMenu.Enable();
    }

    private void DisableAllControls()
    {
        playerControls.Tank.Disable();
        playerControls.BuyMenu.Disable();
    }

    public Player GetPlayer() => player;
    public BaseClass GetPlayerBase() => playerBase;
    public PlayerControls GetPlayerControls() => playerControls;
}
