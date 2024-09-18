using System.Net.Mail;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Player player;
    public Color platformColor;
    


    public int coins;
    public int totalCoins;
    public float lastScore;
    public float highScore;
    public float distance;
    public bool haveSecondChance;

    [SerializeField] private GameObject endGameUI;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(this);
      
        LoadInfo();
        Time.timeScale = 1;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
        if (player.transform.position.x > distance)
            distance = player.transform.position.x;
    }

    public void UnlockPlayer()
    {
        player.playerUnlocked = true;
    }



    public void RestartLevel()
    {
       
        SaveInfo();
        SceneManager.LoadScene("SampleScene");
    }


    // differ total coins and coins
    public void SaveInfo()
    {
        PlayerPrefs.SetInt("Coins", coins);
        


        int savedCoins = PlayerPrefs.GetInt("Coins");
        totalCoins += savedCoins;

        

        PlayerPrefs.SetInt("TotalCoins",totalCoins);

        float score = distance * coins;

        PlayerPrefs.SetFloat("LastScore", score);

        if(PlayerPrefs.GetFloat("HighScore") < score)
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
    }

    public void LoadInfo()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins");
        lastScore = PlayerPrefs.GetFloat("LastScore");
        highScore = PlayerPrefs.GetFloat("HighScore");

    }

    public void SavePlayerColor(float r, float b, float g)
    {
        PlayerPrefs.SetFloat("PlayerColorR", r);
        PlayerPrefs.SetFloat("PlayerColorG", g);
        PlayerPrefs.SetFloat("PlayerColorB", b);
    }

    private void LoadColor()
    {
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        Color newColor = new Color( PlayerPrefs.GetFloat("PlayerColorR"),
                                    PlayerPrefs.GetFloat("PlayerColorG"),
                                    PlayerPrefs.GetFloat("PlayerColorB"),
                                    PlayerPrefs.GetFloat("PlayerColorA",1));

        sr.color = newColor;
    }

    public void OpenEndGameUI()
    {
        Main.instance.SwitchToUI(endGameUI);
    }

}
