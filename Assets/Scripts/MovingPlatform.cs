using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool onPlatform;
    public Transform player;
    public Vector2 positionOnLanding;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
            player = other.transform;
            //positionOnLanding = player.position;
            onPlatform = true;
        }
    }

    void Update()
    {
        if (onPlatform)
        {
            player.position = new Vector2(transform.position.x, player.position.y);
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
            onPlatform = false;
        }
    }
}
