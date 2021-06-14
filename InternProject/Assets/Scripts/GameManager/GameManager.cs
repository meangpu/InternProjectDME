using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] CinemachineVirtualCamera buyModeCam;
    public bool isBuying;
    [SerializeField] private float respawnTime = 15f;
    [SerializeField] private GameObject pausePanel = null;

    [Header("GameOver")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    [SerializeField] float zoomInMax;
    [SerializeField] float zoomOutMax;
    [SerializeField] camTarget camTarScript;
    float NowZoomValue = 15;

    [Header("Victory")]
    [SerializeField] starSetter starScpt;
    [SerializeField] BaseClass baseScpt;

    // GameObject references
    private Player player;
    private BaseClass playerBase;

    public event Action OnBuyModeTrigger;
    public event Action OnCheckWhatCanBuy;

    private float respawnTimeRemaining = 0f;
    private bool isPaused = false;

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
        DisableZoom();
        ResumeGame();

        playerControls.Menu.Pause.performed += _ => HandlePause();
    }

    private void Update()
    {
        // playerControls.BuyMenu.MousePosition.ReadValue<Vector2>();

        CheckZoom();

        if (respawnTimeRemaining == 0f) { return; }

        respawnTimeRemaining = Mathf.Max(respawnTimeRemaining - Time.deltaTime, 0f);
        UIManager.Instance.UpdateRespawnBar(respawnTimeRemaining / respawnTime);
        
        if (respawnTimeRemaining == 0f)
        {
            player.gameObject.SetActive(true);
            UIManager.Instance.ResetRespawnBar();
            PlayerStats.Instance.RespawnPlayer();
        }
    }

    public void CheckZoom()
    {
        float direction = playerControls.BuyMenu.Zoom.ReadValue<float>();
        if (direction > 0)
        {
            
            if (buyModeCam.m_Lens.OrthographicSize > zoomInMax)
            {
                NowZoomValue =  buyModeCam.m_Lens.OrthographicSize - 2f;
                
            }
        }
        else if (direction < 0)
        {
            

            if (buyModeCam.m_Lens.OrthographicSize < zoomOutMax)
            {
                NowZoomValue =  buyModeCam.m_Lens.OrthographicSize + 2f;
            }
            
        }

        buyModeCam.m_Lens.OrthographicSize = Mathf.Lerp(buyModeCam.m_Lens.OrthographicSize, NowZoomValue, Time.deltaTime *15f);
    }

    public void GameOver()
    {
        PauseGame();
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
        isPaused = true;
        pausePanel.SetActive(true);
        DisableAllControls();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pausePanel.SetActive(false);
        EnableAllControls();
    }

    public IEnumerator LevelWon(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);

        starScpt.getStar(checkStar());

        winPanel.SetActive(true);
        PauseGame();
    }

    public int checkStar()
    {
        float basePercent = baseScpt.getPercentHp();
        Debug.Log(basePercent);
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
        OnBuyModeTrigger?.Invoke();

        if (isBuying)
        {
            DisableZoom();
            isBuying = false;
            buyModeCam.m_Priority = 0;
            UIManager.Instance.CloseBuyMenu();
            camTarScript.playMode();
        }
        else
        {
            EnableZoom();
            buyModeCam.m_Priority = 50;
            isBuying = true;
            UIManager.Instance.OpenBuyMenu();
            camTarScript.buyMode();
        }
    }

    public void HandlePlayerDeath()
    {
        respawnTimeRemaining = respawnTime;
        BuyModeSwap();
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
