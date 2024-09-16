using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ingame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI distanceText;

    [SerializeField] private Image heartFull;


    //we can use update, but for better approach, we can use InvokeRepeating

    /*
    private void Update()
    {
        coinText.text = GameManager.instance.coin.ToString();
        distanceText.text = GameManager.instance.distance.ToString("0") + " m";
    }
    */

    private void Start()
    {
        InvokeRepeating("UpdateInfo", 0, .2f);
    }

    private void UpdateInfo()
    {
        coinText.text = GameManager.instance.coin.ToString();
        distanceText.text = GameManager.instance.distance.ToString("0") + " m";
        heartFull.enabled = GameManager.instance.haveSecondChance;

    }


}
