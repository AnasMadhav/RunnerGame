using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> roads;
    [SerializeField] float tileOffset = 60f;
    void Start()
    {
        if(roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    public void MoveRoad()
    {
        GameObject movedRoad = roads[0];
        roads.Remove(movedRoad);
        float newZ = roads[roads.Count - 1].transform.position.z + tileOffset;
        movedRoad.gameObject.GetComponent<ObstacleSpawner>().SpawnObstacle();
       movedRoad.gameObject.GetComponent<CoinSpawner>().SpawnCoins();
        movedRoad.transform.position = new Vector3(0, 0, newZ);
        roads.Add(movedRoad);  
    }
}
