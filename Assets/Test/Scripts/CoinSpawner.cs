using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] int maxCoinCount = 10, minCoinCount = 5;
    [SerializeField]  float widthOffset = 0.5f,heightOffset =1f;
    [SerializeField] List<int> xPoints = new List<int>{ -2, 0, 2 ,0};
    GameObject[] coins;
    private void Awake()
    {
        coins = new GameObject[maxCoinCount];
        SpawnCoins();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnCoins()
    {
        if(coins != null)
        {
            ResetCoins();
        }
        int coinCount = Random.Range(minCoinCount,maxCoinCount);
        for (int i = 0; i < coinCount; i++)
        {
            coins[i] = Instantiate(coinPrefab,gameObject.transform);
            coins[i].transform.position = GetCoinPosition(gameObject.GetComponent<Collider>());
        }
       // Debug.Log("Coins Respawned");
    }
    public void ResetCoins()
    {
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
    }
    Vector3 GetCoinPosition(Collider collider)
    {
        // Vector3 point = new Vector3(Random.Range(collider.bounds.min.x+widthOffset,collider.bounds.max.x-widthOffset),heightOffset, 
        //  Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        int val = Random.Range(0, 3);
        Vector3 point = new Vector3(xPoints[val], heightOffset,
           Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        return point; 
    }
}
