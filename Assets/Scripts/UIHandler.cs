using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject pauseScreenPanel;
    [SerializeField] Text pauseMenuText;
    public void OpenPauseMenu(string text = "Game Paused")
    {
        pauseScreenPanel.SetActive(true);
        pauseMenuText.text = text;
        Time.timeScale = 0;
    }
    public void ReturnToGame()
    {
        pauseScreenPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
