using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("GameOver")]
    [SerializeField] GameObject gameOverPanel;

    private void Start() 
    {
        ResumeGame();
    }

    public void GameOver()
    {
        PauseGame();
        gameOverPanel.SetActive(true);
    }



    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
