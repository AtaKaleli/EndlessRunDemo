using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{


    [SerializeField] private GameObject coinPref;

    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;
    private int amountOfCoins;

    [SerializeField] private SpriteRenderer[] coinImages;

    void Start()
    {
        for (int i = 0; i < coinImages.Length; i++)
        {
            coinImages[i].sprite = null;
        }

        amountOfCoins = Random.Range(minCoins, maxCoins);
        int additionalOffset = amountOfCoins / 2;
        
        for (int i = 0; i < amountOfCoins; i++)
        {
            Vector3 offset = new Vector2(i - additionalOffset, 0);
            Instantiate(coinPref, transform.position + offset, Quaternion.identity,transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
