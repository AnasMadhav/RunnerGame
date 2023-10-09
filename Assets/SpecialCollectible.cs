using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCollectible : MonoBehaviour
{
    [SerializeField] GameObject brokenGlass;
    CollectibleData CollectibleData;
    void Start()
    {
      CollectibleData = GetComponent<CollectibleData>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Instantiate(brokenGlass, transform.position, transform.rotation);
            Destroy(gameObject);
            CollectibleData.LoadData();
            
        }
    }

}
