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



    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
