using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerMovement playerMovement;
    [SerializeField] UIManager UIManager;
    [SerializeField] RoadSpawner RoadSpawner;
    [SerializeField] TextMeshProUGUI coinCountText,totalCoin,collectibleCountText,timerCountText,healthCountText;
    [SerializeField] float gameOverDelay = 2.8f, collectibleDelay,collectibleMapDuration;
    [SerializeField] GameObject collectibleUi;
    [SerializeField] Image collectibleUiImage;
    [SerializeField] TextMeshProUGUI collectibleName,collectibleInfo;
    List<GameObject> collectibleMap;
    GameObject collectiblePlatfrom;
    public GameObject MainCamera;
    public GameObject UICamera;
    public GameObject UICharacter;
    public static int coinCount, totalCoinCount,collectibleCount;

    //
    [SerializeField] TextMeshProUGUI QuizCoinText;
    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        playerMovement = player.GetComponent<PlayerMovement>();
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
      //  Debug.Log(playerMovement.currentTime);
        TimerUpdate();
    }
    public void CoinUpdate()
    {
        coinCount++;
        coinCountText.text = coinCount.ToString();
        //player.effect.play
        totalCoinCount = PlayerPrefs.GetInt("Coin1") + 1;
        PlayerPrefs.SetInt("Coin1", totalCoinCount);
        totalCoin.text = PlayerPrefs.GetInt("Coin1").ToString();
    }

    public void CollectibleUpdate()
    {
        collectibleCount++;
        collectibleCountText.text = collectibleCount.ToString();
    }

    public void TimerUpdate()
    {
        timerCountText.text = playerMovement.currentTime.ToString();
    }
    public void DisableTimer()
    {
        timerCountText.enabled = false; 
    }

    //---------------------------------GAMEPLAY---------------------------------------------
   public void HealthUpdate(int health)
   {
        healthCountText.text = health.ToString();
   }
    public void DistanceUpdate()
    {
        int lastDistance = PlayerPrefs.GetInt("Distance");
        if(playerMovement.distanceCovered > lastDistance)
        {
            PlayerPrefs.SetInt("Distance",playerMovement.distanceCovered);
        }
    }
    public void GameOver()
    {
        DistanceUpdate();
        StartCoroutine(GameOverUi());
        QuizCoinText.text = coinCount.ToString();
    }
    IEnumerator GameOverUi()
    {
        yield return new WaitForSeconds(gameOverDelay);
        UIManager.EnablePanel("Popup");
        MainCamera.SetActive(false);
        UICamera.SetActive(true);
    }
    // COLLECTIBLE UI
    public void CollectibleUiOpen()
    {
        UIManager.EnablePanel("SpecialCollectiblePanel");
        playerMovement.isMovable = false;
        playerMovement.animator.SetFloat("Speed", 0);
    }

    public void LoadCollectibleData(string name,Sprite image,string info,GameObject platform,List<GameObject>map)
    {
        collectibleName.text = name;
        collectibleUiImage.sprite = image;
        collectibleInfo.text = info;
        collectiblePlatfrom = platform;
        collectibleMap = map;
    }
    public void CollectibleClose()
    {
        StartCoroutine(CollectibleUiClose());
    }

    IEnumerator ChangeMap()
    {
        RoadSpawner.LoadCollectibleMap(collectiblePlatfrom,collectibleMap);
        yield return new WaitForSeconds(collectibleMapDuration);
        RoadSpawner.LoadOriginalMap();
      
    }
    IEnumerator CollectibleUiClose()
    {
        UIManager.EnablePanel("HudPanel");
        StartCoroutine(ChangeMap());    
        yield return new WaitForSeconds(collectibleDelay);
        playerMovement.isMovable = true;
        playerMovement.forwardSpeed = 8f;
        playerMovement.animator.SetFloat("Speed", playerMovement.initialAnimationSpeed);
        CollectibleUpdate();
    }
    public void CoinDoubling(int No)
    {
        totalCoinCount = PlayerPrefs.GetInt("Coin1");
        int DoublecoinCount = coinCount * No;
        PlayerPrefs.SetInt("Coin1", totalCoinCount + DoublecoinCount);
        QuizCoinText.text = DoublecoinCount.ToString();
        
    }
}
