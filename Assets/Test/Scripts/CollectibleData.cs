using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleData : MonoBehaviour
{
    [SerializeField] string name, info;
    [SerializeField] Image image;
    [SerializeField] List<GameObject> map;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").gameObject.GetComponent<GameManager>();
    }
    
    public void LoadData()
    {
        gameManager.LoadCollectibleData(name,image, name, map);
    }
}
