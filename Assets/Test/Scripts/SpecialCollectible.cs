using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCollectible : MonoBehaviour
{
    [SerializeField] GameObject brokenGlass;
    CollectibleData CollectibleData;
    SpecialsSpawner SpecialsSpawner;
    void Start()
    {
      CollectibleData = GetComponent<CollectibleData>();
      SpecialsSpawner = GameObject.FindWithTag("SpawnManager").gameObject.GetComponent<SpecialsSpawner>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Instantiate(brokenGlass, transform.position, transform.rotation);
            PlayerPrefs.SetInt(gameObject.name, 1);
            Destroy(gameObject);
            CollectibleData.LoadData();
            SpecialsSpawner.RemoveCollectible(gameObject);
        }
    }

}
