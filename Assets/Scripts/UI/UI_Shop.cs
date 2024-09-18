using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public struct ColorToSell
{
    public int price;
    public Color color;
}

public enum ColorType
{
    PlayerColor, PlatformColor
}

public class UI_Shop : MonoBehaviour
{
    [Header("Platform Colors")]
    [SerializeField] private ColorToSell[] platformColors;
    [SerializeField] private GameObject platformColorButton;
    [SerializeField] private Transform platformColorButtonParent;
    [SerializeField] private Image platformDisplay;


    [Header("Player Colors")]
    [SerializeField] private ColorToSell[] playerColors;
    [SerializeField] private GameObject playerColorButton;
    [SerializeField] private Transform playerColorButtonParent;
    [SerializeField] private Image playerDisplay;


    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI notifyText;

    private void Start()
    {

        UpdateTotalCoins();

        for (int i = 0; i < platformColors.Length; i++)
        {

            Color color = platformColors[i].color;
            int price = platformColors[i].price;

            GameObject newButton = Instantiate(platformColorButton, platformColorButtonParent);

            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("0");


            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price,ColorType.PlatformColor));

        }

        for (int i = 0; i < playerColors.Length; i++)
        {

            Color color = playerColors[i].color;
            int price = playerColors[i].price;

            GameObject newButton = Instantiate(playerColorButton, playerColorButtonParent);

            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("0");


            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.PlayerColor));

        }
    }

    public void PurchaseColor(Color color, int price, ColorType colorType)
    {
        if (EnoughMoney(price))
        {
            if(colorType == ColorType.PlatformColor)
            {
                GameManager.instance.platformColor = color;
                platformDisplay.color = color;

            }
            else if(colorType == ColorType.PlayerColor)
            {
                GameManager.instance.player.GetComponent<SpriteRenderer>().color = color;
                GameManager.instance.SavePlayerColor(color.r, color.g, color.b);
                playerDisplay.color = color;
            }

            StartCoroutine(Notify("Purchased successful", 1));
            
        }
        else
        {
            StartCoroutine(Notify("Not enough money", 1));

        }


    }

    private void UpdateTotalCoins()
    {
        totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins").ToString();
    }

    private bool EnoughMoney(int price)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins");

        if(totalCoins > price)
        {
            int newAmountCoins = totalCoins - price;
            GameManager.instance.totalCoins = newAmountCoins;
            PlayerPrefs.SetInt("TotalCoins",newAmountCoins);
            UpdateTotalCoins();
            return true;
        }

        return false;
    }


    private IEnumerator Notify(string text, float seconds)
    {

        notifyText.text = text;
        yield return new WaitForSeconds(seconds);
        notifyText.text = "Click to buy";

    }
}
