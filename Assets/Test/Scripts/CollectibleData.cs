using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleData : MonoBehaviour
{
    [SerializeField] string name, info;
    [SerializeField] Sprite image;
    public List<GameObject> map;
    [SerializeField] GameObject platform;
    [SerializeField] float tileOffset = 30f;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").gameObject.GetComponent<GameManager>();
    }
    
    public void LoadData()
    {
        gameManager.LoadCollectibleData(name,image, info,platform,map);
        gameManager.CollectibleUiOpen();
    }
}
