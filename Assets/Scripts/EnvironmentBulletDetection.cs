using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBulletDetection : MonoBehaviour
{

    public GameObject bulletVFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("Wall detected bullet");
            //MasterPool.SpawnBulletVFX(bulletVFX, collision.transform.position, collision.transform.rotation, "Impact");
        }
        
        
    }
}
