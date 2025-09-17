using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentEnegy;
    [SerializeField] private int energyThreshold = 20;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject enemy;
    private bool bossCalled = false;
    [SerializeField] private Image energyBar;
    [SerializeField] GameObject gameUI;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseGameMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject guideMenu;
    [SerializeField] private GameObject guideMenuPause;
    [SerializeField] private GameObject gameWinMenu;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private GameObject red;
    [SerializeField] private Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        
        currentEnegy = 0;
        UpdateEnergy();
        boss.SetActive(false);
        MainMenu();
        audioManager.StopAudioGame();
        cam.m_Lens.OrthographicSize = 7f;
        red.SetActive(false);
    }



    public void AddEnergy()
    {
        if(bossCalled) return;  

        currentEnegy += 1;
        UpdateEnergy();
        if (currentEnegy == energyThreshold)
        {
            CallBoss();
            
        }
    }

    private void CallBoss()
    {
        bossCalled = true;
        boss.SetActive(true);
        enemy.SetActive(false);
        gameUI.SetActive(false);
        audioManager.PlayBossAudio();
        cam.m_Lens.OrthographicSize = 11f;
        red.SetActive(true);
        player.Heal(150f);
        
        

    }

    private void UpdateEnergy() 
    {
        if (energyBar != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentEnegy / energyThreshold);
            energyBar.fillAmount = fillAmount;
        }
        if (energyText != null)
        {
            if (currentEnegy > 0)
            {
                energyText.text = currentEnegy.ToString() + "/20";
            }
            else
            {
                energyText.text = "0/20";
            }

        }

    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        guideMenu.SetActive(false);
        guideMenuPause.SetActive(false);
        gameWinMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);   
        mainMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        guideMenu.SetActive(false);
        guideMenuPause.SetActive(false);
        gameWinMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameWinMenu()
    {
        gameOverMenu.SetActive(false);
        mainMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        guideMenu.SetActive(false);
        guideMenuPause.SetActive(false);
        gameWinMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseGameMenu()
    {
        pauseGameMenu.SetActive(true);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        guideMenu.SetActive(false);
        guideMenuPause.SetActive(false);
        gameWinMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        guideMenu.SetActive(false);
        guideMenuPause.SetActive(false);
        gameWinMenu.SetActive(false);
        Time.timeScale = 1f;
        audioManager.PlayDefaultAudio();
    }

    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        guideMenu.SetActive(false);
        guideMenuPause.SetActive(false);
        gameWinMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GuideMenu()
    {
        guideMenu.SetActive(true); 
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        guideMenuPause.SetActive(false);
        gameWinMenu.SetActive(false);
        Time.timeScale = 0f;
        
    }

    public void GuideMenuPause()
    {
        guideMenuPause.SetActive(true);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        guideMenu.SetActive (false);
        gameWinMenu.SetActive(false);
        Time.timeScale = 0f;

    }

}
