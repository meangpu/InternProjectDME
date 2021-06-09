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
    }

    private void Start()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (respawnTime == 0f) { return; }

        respawnTimeRemaining -= Time.deltaTime;
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
            isBuying = false;
            buyModeCam.m_Priority = 0;
            UIManager.Instance.CloseBuyMenu();
        }
        else
        {
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

    public Player GetPlayer() => player;
    public BaseClass GetPlayerBase() => playerBase;
}
