using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerHurtBox")
        {
            gameManager.FinishGame();
            Debug.Log("Finished!");
        }
    }
}
