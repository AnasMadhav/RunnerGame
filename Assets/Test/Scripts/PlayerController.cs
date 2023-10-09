using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] RoadSpawner roadSpawner;
    PlayerMovements playerMovements;
    void Start()
    {
        playerMovements = gameObject.GetComponent<PlayerMovements>();
    }

    void Update()
    {
        
    }

    void adjustPlayerSpeed(float value)
    {
        playerMovements.moveSpeedZ *= value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnTriggered();
            roadSpawner.lastSpawnTriggeredPos = other.gameObject.transform.position.z;
        }
    }
}
