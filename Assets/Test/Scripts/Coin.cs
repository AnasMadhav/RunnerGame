using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").gameObject.GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            return;
        }
      //  if (other.gameObject.CompareTag("Magnet"))
        {
         //   StartCoroutine(AttractToPlayer(gameObject));
        //    Debug.Log("Magnet");
        }

        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.CoinUpdate();

            Destroy(gameObject);
        }
    }

    IEnumerator AttractToPlayer(GameObject player)
    {
        while (gameObject.activeInHierarchy)
        {
            gameObject.transform.Translate(player.transform.position);
           
        }
        yield return null;
    }
}
