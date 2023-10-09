using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefab = new GameObject[2];
    [SerializeField] int maxObstacleCount = 3, minObstacleCount = 1;
    [SerializeField] float widthOffset=1;
    GameObject[] obstacles;
    void Awake()
    {
        obstacles = new GameObject[maxObstacleCount];
        SpawnObstacle();
    }
    void Update()
    {
        
    }

    public void SpawnObstacle()
    {
        if(obstacles != null)
        {
            ResetObstacles();
        }
        int obstacleCount = Random.Range(minObstacleCount, maxObstacleCount);
        for(int i = 0; i < obstacleCount; i++)
        {
            obstacles[i] = Instantiate(obstaclePrefab[Random.Range(0,obstaclePrefab.Length)],gameObject.transform);
            obstacles[i].transform.position = GetObstaclePosition(gameObject.GetComponent<Collider>());
        }
    }
    public void ResetObstacles()
    {
        foreach(GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }
    Vector3 GetObstaclePosition(Collider collider)
   {
       Vector3 point = new Vector3(Random.Range(collider.bounds.min.x + widthOffset, collider.bounds.max.x - widthOffset), collider.bounds.min.y,
          Random.Range(collider.bounds.min.z, collider.bounds.max.z));
       return point;
   }
}

