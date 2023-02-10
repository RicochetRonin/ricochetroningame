using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePopupScript : MonoBehaviour
{


    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private GameObject interactText;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag) && 
            Physics2D.Raycast(transform.position, collision.transform.position - transform.position, interactDistance))
        {
            interactText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            interactText.SetActive(false);
        }   
    }
}
