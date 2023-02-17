using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSpawn : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.newSceneLoaded == true && GameManager.checkPointActive == false)
        {
            GameManager.lastCheckPointPos = new Vector2(transform.position.x, transform.position.y);
            Debug.LogFormat("Starting Spawn Set to: {0}", GameManager.lastCheckPointPos);
        }
    }
}
