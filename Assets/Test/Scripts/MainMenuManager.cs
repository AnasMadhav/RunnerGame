using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] TextMeshProUGUI coinCountText,specialCountText;
    void Start()
    {
        uiManager.EnablePanel("MainPanel");
        coinCountText.text = PlayerPrefs.GetInt("Coin1").ToString();
        specialCountText.text = PlayerPrefs.GetInt("Collectible").ToString();
    }

    void Update()
    {
        
    }
}
