using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    private int coin;
    public Color platformColor;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }


    public void CollectCoin()
    {
        coin++;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    
    
}
