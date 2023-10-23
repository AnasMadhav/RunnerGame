using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialsSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> specialCollectibles;
    float spawnRange = 100f;
    [SerializeField, Range(0, 100)] float spawnPercent;
    [SerializeField] float widthOffset = 0.5f;
    [SerializeField] float heightOffset = 1.2f;
   // List<GameObject> spawnedCollectibles;
    Collider _collider;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Fetching the road collider for spawn boundary
    public void SetRoad(GameObject road)
    {
        _collider = road.GetComponent<Collider>();
    }

    //Get a random vector inside the road
    Vector3 GetCollectiblePosition(Collider collider)
    {
        Vector3 point = new Vector3(Random.Range(collider.bounds.min.x + widthOffset, collider.bounds.max.x - widthOffset), heightOffset,
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        return point;
    }

    public void SpawnCollectibles(GameObject road)
    {
      float spawnChance = Random.Range(0, spawnRange);
      if(spawnChance < spawnPercent && specialCollectibles != null)
      {
            if (specialCollectibles.Count > 0)
            {
                GameObject specials = Instantiate(specialCollectibles[Random.Range(0, specialCollectibles.Count)], GetCollectiblePosition(_collider),
                   Quaternion.identity, road.transform);
                if (PlayerPrefs.GetInt(specials.name) == 1)
                {
                    Destroy(specials);
                    PlayerPrefs.SetInt("Collectible", PlayerPrefs.GetInt("Collectible") + 1);
                    Debug.Log("Multiple Found");
                }
            }
      }
    }

    public void RemoveCollectible(GameObject collectible)
    {
        specialCollectibles.Remove(collectible);
    }


}
