using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject pauseScreenPanel;
    [SerializeField] Text pauseMenuText;
    [SerializeField] GameObject infoPanel;
    [SerializeField] Text objectNameText;
    [SerializeField] Text objectDescriptionText;
    [SerializeField] Text objectHealthText;
    public void HideObjectDetailsPanel()
    {
        objectNameText.enabled = false;
        objectDescriptionText.enabled = false;
        objectHealthText.enabled = false;
    }
    public void PrintObjectDetails(Health targetObject)
    {
        if (targetObject == null)
        {
            HideObjectDetailsPanel();
            return;
        }
        objectNameText.enabled = true;
        objectDescriptionText.enabled = true;
        objectHealthText.enabled = true;
        objectNameText.text = targetObject.objectname;
        objectDescriptionText.text = targetObject.description;
        objectHealthText.text = "Health: " + (int)targetObject.health + "/" + (int)targetObject.maxHealth;
    }
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
