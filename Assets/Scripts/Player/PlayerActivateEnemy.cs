using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivateEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            var transform = coll.GetComponent<Transform>();
            for (int a = 0; a < transform.childCount; a++)
            {
                transform.GetChild(a).gameObject.SetActive(true);
            }
            
            /*
            foreach (GameObject child in transform)
            {
                child.SetActive(true);
            }
            */
        }
    }
    
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            var transform = coll.GetComponent<Transform>();
            for (int a = 0; a < transform.childCount; a++)
            {
                transform.GetChild(a).gameObject.SetActive(false);
            }
            
            /*
            foreach (GameObject child in transform)
            {
                child.SetActive(false);
            }
            */
        }
    }
}
