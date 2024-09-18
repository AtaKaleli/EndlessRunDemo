using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Endgame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Start()
    {
        GameManager.instance.SaveInfo();
        UpdateInfo();
        Time.timeScale = 0;
    }

    private void UpdateInfo()
    {
        if (GameManager.instance.coins == 0)
            return;

        coinText.text = "Coins: " + GameManager.instance.coins.ToString();
        distanceText.text = "Distance: " + GameManager.instance.distance.ToString("0") + " m";
        scoreText.text = "Score: " + PlayerPrefs.GetFloat("LastScore").ToString("0");
       
    }

}
