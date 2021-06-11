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

    [Header("GameOver")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    // GameObject references
    private Player player;
    private BaseClass playerBase;

    public event Action OnBuyModeTrigger;
    public event Action OnCheckWhatCanBuy;

    private float respawnTimeRemaining = 0f;

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
    }

    private void Update()
    {
        // Perform zoomy zoomy

        if (respawnTimeRemaining == 0f) { return; }

        respawnTimeRemaining = Mathf.Max(respawnTimeRemaining - Time.deltaTime, 0f);
        UIManager.Instance.UpdateRespawnBar(respawnTimeRemaining / respawnTime);
        checkZoom();
        if (respawnTimeRemaining == 0f)
        {
            player.gameObject.SetActive(true);
            UIManager.Instance.ResetRespawnBar();
            PlayerStats.Instance.RespawnPlayer();
        }
    }

    public void checkZoom()
    {
        float direction = playerControls.BuyMenu.Zoom.ReadValue<float>();
        if (direction > 0)
        {
            Debug.Log(direction);
        }
        else
        {
            Debug.Log("direction");
        }
    }

    public void GameOver()
    {
        PauseGame();
        gameOverPanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public IEnumerator LevelWon(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);

        winPanel.SetActive(true);
        PauseGame();
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
        }
        else
        {
            EnableZoom();
            buyModeCam.m_Priority = 50;
            isBuying = true;
            UIManager.Instance.OpenBuyMenu();
        }
    }

    public void HandlePlayerDeath()
    {
        respawnTimeRemaining = respawnTime;
        BuyModeSwap();
    }

    private void DisableZoom()
    {
        playerControls.BuyMenu.Zoom.Disable();
    }

    private void EnableZoom()
    {
        playerControls.BuyMenu.Zoom.Enable();
    }

    public Player GetPlayer() => player;
    public BaseClass GetPlayerBase() => playerBase;
    public PlayerControls GetPlayerControls() => playerControls;
}
