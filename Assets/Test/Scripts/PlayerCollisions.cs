using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
  GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").gameObject.GetComponent<GameManager>();
    }

   private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            gameManager.CoinUpdate();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Over");

        }
    }
    
}
