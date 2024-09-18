using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static Main instance;

    [SerializeField] private GameObject[] UIElements;

    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private bool gamePaused;

    [SerializeField] private UI_Settings[] volumeController;

    private void Awake()
    {
        
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        Time.timeScale = 0;
    }
    private void Start()
    {
        

        for (int i = 1; i < UIElements.Length; i++)
        {
            UIElements[i].SetActive(false);
        }

        UIElements[0].SetActive(true);
        UpdateInfo();

        for (int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<UI_Settings>().SetupVolumeSlider();
        }

    }

    public void SwitchToUI(GameObject targetUI)
    {
        for (int i = 0; i < UIElements.Length; i++)
        {
            UIElements[i].SetActive(false);
        }

        targetUI.SetActive(true);
        AudioManager.instance.PlaySFX(5);
        UpdateInfo();
    }

    public void StartGameButton()
    {
        GameManager.instance.UnlockPlayer();
    }

    
    public void PauseGameButton()
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
    }

    public void RestartGameButton()
    {
        GameManager.instance.RestartLevel();
    }

    private void UpdateInfo()
    {
        coinsText.text = PlayerPrefs.GetInt("TotalCoins").ToString();
        //coinsText.text = GameManager.instance.totalCoins.ToString("0");
        lastScoreText.text = "Last Score: " + GameManager.instance.lastScore.ToString("0");
        bestScoreText.text = "Best Score: " + GameManager.instance.highScore.ToString("0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
