using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("GameOver")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    // GameObject references
    private PlayerGetHit player;
    private BaseClass playerBase;

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
    }

    private void Start()
    {
        ResumeGame();
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

    public void LevelWon()
    {
        PauseGame();
        winPanel.SetActive(true);
    }

    public PlayerGetHit GetPlayer() => player;
    public BaseClass GetPlayerBase() => playerBase;
}
