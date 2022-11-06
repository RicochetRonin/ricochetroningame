using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {  
        if (other.tag == "PlayerHurtBox")
        {
            GameManager.lastCheckPointPos = transform.position;
            Debug.Log("Checkpoint!");
        } 
    }
}
