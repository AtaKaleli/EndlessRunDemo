using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Player player;
    public Color platformColor;


    public int coin;
    public float distance;
    public bool haveSecondChance;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }

    private void Update()
    {
        print(haveSecondChance);
        if (player.transform.position.x > distance)
            distance = player.transform.position.x;
    }

    public void UnlockPlayer()
    {
        player.playerUnlocked = true;
    }

    

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    
    
}
