using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    [SerializeField] private GameObject[] UIElements;

    private void Start()
    {
        for (int i = 1; i < UIElements.Length; i++)
        {
            UIElements[i].SetActive(false);
        }

        UIElements[0].SetActive(true);
    }

    public void SwitchToUI(GameObject targetUI)
    {
        for (int i = 0; i < UIElements.Length; i++)
        {
            UIElements[i].SetActive(false);
        }

        targetUI.SetActive(true);
    }

    public void StartGame()
    {
        GameManager.instance.UnlockPlayer();
    }

    




}
