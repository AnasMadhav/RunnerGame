using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offSet;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = player.transform.position + offSet;
    }
}
