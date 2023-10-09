using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] GameObject platforms;
    [SerializeField] List<GameObject> roads;
    [SerializeField] List<GameObject> roadTile;
    [SerializeField] float tileOffset = 30f;
    [SerializeField] public float lastSpawnTriggeredPos;
    GameObject spawnRoad;
    void Start()
    {
        roads = roadTile;
        if(roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    public void LoadCollectibleMap(GameObject platform,List<GameObject>map)
    {
        roads.Clear();
        platforms.SetActive(false);
        spawnRoad =  Instantiate(platform);
    
        for (int i = 0; i < spawnRoad.transform.childCount; i++)
        {
            roads.Add(spawnRoad.transform.GetChild(i).gameObject);
            roads[i].transform.position += Vector3.forward * lastSpawnTriggeredPos * 2;
        }
    }
    public void LoadOriginalMap()
    {
        Destroy(spawnRoad);
        platforms.SetActive(true);
        roads.Clear();
        roads = roadTile;
        foreach(GameObject road in roads)
        {
            road.transform.position += Vector3.forward * lastSpawnTriggeredPos;
        }
    }

    public void MoveRoad()
    {
        GameObject movedRoad = roads[0];
        roads.Remove(movedRoad); 
        float newZ = roads[roads.Count - 1].transform.position.z + tileOffset;
        Debug.Log(newZ);
        movedRoad.gameObject.GetComponent<ObstacleSpawner>().SpawnObstacle();
        movedRoad.gameObject.GetComponent<CoinSpawner>().SpawnCoins();
        movedRoad.transform.position = new Vector3(0, 0, newZ);
        Debug.Log( movedRoad.name+"  "+movedRoad.transform.position.z);
        roads.Add(movedRoad);
    }
}
