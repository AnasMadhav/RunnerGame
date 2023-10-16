using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] bool isTransition;
    [SerializeField] GameObject[] keralaMaps;
    [SerializeField] int oldKeralaDistance, midKeralaDistance, newKeralaDistance;
    [SerializeField] GameObject platforms;
    [SerializeField] List<GameObject> roads;
    [SerializeField] List<GameObject> roadTile;
    [SerializeField] float tileOffset = 30f;
    [SerializeField] public float lastSpawnTriggeredPos;
    GameObject spawnRoad;
    SpecialsSpawner specialsSpawner;
    void Start()
    {
        isTransition = false;
        specialsSpawner = gameObject.GetComponent<SpecialsSpawner>();
        //  roads = roadTile;
        for (int i = 0; i < roadTile.Count; i++)
        {
            roads.Add(platforms.transform.GetChild(i).gameObject);
            roads[i].transform.position = Vector3.forward * ((i-1) * tileOffset);
        }
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
        ObstacleRemoveAtInitial(roads[1]);
    }

    public void LoadCollectibleMap(GameObject platform,List<GameObject>map)
    {
        roads.Clear();
        platforms.SetActive(false);
        spawnRoad =  Instantiate(platform);
        for (int i = 0; i < spawnRoad.transform.childCount; i++)
        {
            roads.Add(spawnRoad.transform.GetChild(i).gameObject);
            roads[i].transform.position = Vector3.forward * (lastSpawnTriggeredPos + (tileOffset * i));
        }
        ObstacleRemoveAtInitial(roads[1]);
    }
    public void LoadOriginalMap()
    {
        Destroy(spawnRoad);
        roads.Clear();
        platforms.SetActive(true);
      //  roads = roadTile;
        for (int i = 0; i < roadTile.Count; i++)
        {
            roads.Add(platforms.transform.GetChild(i).gameObject);
            roads[i].transform.position = Vector3.forward* (lastSpawnTriggeredPos + (tileOffset* i-1));
        }
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
        ObstacleRemoveAtInitial(roads[1]);
    }
    
   
    public void ObstacleRemoveAtInitial(GameObject road)
    {
        road.GetComponent<ObstacleSpawner>().ResetObstacles();
    }
    public void MoveRoad()
    {
        if (!isTransition)
        {
            GameObject movedRoad = roads[0];
            roads.Remove(movedRoad);
            float newZ = roads[roads.Count - 1].transform.position.z + tileOffset;
            movedRoad.gameObject.GetComponent<ObstacleSpawner>().SpawnObstacle();
            movedRoad.gameObject.GetComponent<CoinSpawner>().SpawnCoins();
            specialsSpawner.SetRoad(movedRoad);
            specialsSpawner.SpawnCollectibles(movedRoad);
            movedRoad.transform.position = new Vector3(0, 0, newZ);
            roads.Add(movedRoad);
        }
       
    }
}
