using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;
    [SerializeField] RoadSpawner RoadSpawner;
    [SerializeField] TextMeshProUGUI coinCountText,totalCoin,collectibleCountText;
    [SerializeField] float gameOverDelay = 2.8f, collectibleDelay;
    [SerializeField] GameObject collectibleUi;
    Image collectibleIcon;
    TextMeshProUGUI collectibleName,collectibleInfo;
    List<GameObject> collectibleMap;
    [SerializeField] List<GameObject> road;
    public static int coinCount, totalCoinCount,collectibleCount;
    void Start()
    {
    
        coinCount = 0;
        UIManager.EnablePanel("HudPanel");

        if(!PlayerPrefs.HasKey("Coin1"))
        {
            PlayerPrefs.SetInt("Coin1", 0);
        }
        totalCoin.text = PlayerPrefs.GetInt("Coin1").ToString();

        if (!PlayerPrefs.HasKey("Collectible"))
        {
            PlayerPrefs.SetInt("Collectible", 0);
        }
        collectibleCount = PlayerPrefs.GetInt("Collectible");
        collectibleCountText.text = collectibleCount.ToString();
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

    public void CollectibleUpdate()
    {
        collectibleCount++;
        collectibleCountText.text = collectibleCount.ToString();
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
    // COLLECTIBLE UI
    public void CollectibleUiOpen()
    {
        UIManager.EnablePanel("SpecialCollectiblePanel");
    }

    public void LoadCollectibleData(string name,Image image,string info,List<GameObject> map)
    {
        collectibleName.text = name;
        collectibleIcon = image;
        collectibleInfo.text = info;
        collectibleMap = map;
    }
    public void CollectibleClose()
    {
        StartCoroutine(CollectibleUiClose());
    }

    IEnumerator ChangeMap()
    {

        yield return new WaitForSeconds(collectibleDelay);
    }
    IEnumerator CollectibleUiClose()
    {
        UIManager.EnablePanel("HudPanel");
        //Load map
        yield return new WaitForSeconds(collectibleDelay);
        CollectibleUpdate();
    }
}
