using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialoguePopupScript : MonoBehaviour
{


    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject dialogueText;
    [SerializeField] private GameObject backerImage;
    public LayerMask groundMask;
    public bool inRange = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag) && !Physics2D.Raycast(transform.position,
            collision.transform.position - transform.position,
            Vector2.Distance(transform.position, collision.transform.position), groundMask))
        {
            //interactText.SetActive(true);
            dialogueText.SetActive(true);
            backerImage.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            //interactText.SetActive(false);
            dialogueText.SetActive(false);
            backerImage.SetActive(false);
            inRange = false;
        }   
    }
}
