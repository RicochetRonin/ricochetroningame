using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 respawnPoint;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
        /*playerHealth = GetComponent<PlayerHealth>();*/
        playerHealth = GetComponentInChildren<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHealth.getIsDead())
        {
            Debug.Log("Player is dead at: " + transform.position);
            transform.position = respawnPoint;
            Debug.Log("Player's current position after set to respawn point: " + transform.position);
            playerHealth.setIsDead(false);
            playerHealth.resetPlayer();
        }

        // This is resetting ronin's position, but at random locations
        // getCanTakeDamage and setCanTakeDamage may be called in different script
        // but it's working...
        /*        if (!playerHealth.getCanTakeDamage())
                {
                    transform.position = respawnPoint;
                    playerHealth.setCanTakeDamage(true);
                }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
            Debug.Log(respawnPoint);
        }
    }
}
