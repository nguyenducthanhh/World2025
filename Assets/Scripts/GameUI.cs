using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public void StartGame()
    {
        gameManager.StartGame();
    }

    public void GuideMenu()
    {
        gameManager.GuideMenu();
    }

    public void GuideMenuPause()
    {
        gameManager.GuideMenuPause();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        gameManager.ResumeGame();
    }

    public void PauseGame()
    {
        gameManager.PauseGameMenu();
    }

    public void WinGame()
    {
        gameManager.GameWinMenu();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
