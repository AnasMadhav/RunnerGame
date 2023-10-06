using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;
    [SerializeField] TextMeshProUGUI coinCountText,totalCoin;
    [SerializeField] float gameOverDelay = 2.8f;
    public static int coinCount, totalCoinCount;
    void Start()
    {
    
        coinCount = 0;
        UIManager.EnablePanel("HudPanel");

        if(!PlayerPrefs.HasKey("Coin1"))
        {
            PlayerPrefs.SetInt("Coin1", 0);
        }
        totalCoin.text = PlayerPrefs.GetInt("Coin1").ToString();
    }

   
    void Update()
    {
        
    }
    public void CoinUpdate()
    {
        coinCount++;
        coinCountText.text = coinCount.ToString();

        totalCoinCount = PlayerPrefs.GetInt("Coin1") + 1;
        PlayerPrefs.SetInt("Coin1", totalCoinCount);
        totalCoin.text = PlayerPrefs.GetInt("Coin1").ToString();
    }

    //---------------------------------GAMEPLAY---------------------------------------------
    public void GameOver()
    {
        StartCoroutine(GameOverUi());
    }
    IEnumerator GameOverUi()
    {
        yield return new WaitForSeconds(gameOverDelay);
        UIManager.EnablePanel("GameOverPanel");
    }
}
