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
    public bool isBuying = false;

    [Header("GameOver")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    // GameObject references
    private PlayerGetHit player;
    private BaseClass playerBase;

    private PlayerControls playerControls;

    public event Action<bool> OnBuyModeTrigger;
    public event Action OnCheckWhatCanBuy;

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

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGetHit>();
        playerBase = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseClass>();

        playerControls = new PlayerControls();
    }

    private void Start()
    {
        ResumeGame();

        playerControls.BuyMenu.BuyMode.performed += _ => BuyModeSwap();
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
        Debug.Log("TAB");

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

        OnBuyModeTrigger?.Invoke(isBuying);
    }


    public PlayerGetHit GetPlayer() => player;
    public BaseClass GetPlayerBase() => playerBase;
}
