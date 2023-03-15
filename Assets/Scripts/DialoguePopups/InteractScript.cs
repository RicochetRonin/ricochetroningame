using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InteractScript : MonoBehaviour
{
    public PlayerControls playerControls;

    [SerializeField] private GameObject dialogueText;

    public DialoguePopupScript dialoguePopup;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Interaction.Interact.performed += _ => EnableText();
    }
    // Update is called once per frame
    void Update()
    {
        if(dialogueText.GetComponent<NPCDialogueScript>().dialogueFinished)
        {
            DisableText();
        }
    }
    void EnableText()
    {
        //Debug.Log("Enable text");
        if (dialoguePopup.inRange)
        {
            dialogueText.SetActive(true);
            this.GetComponent<TextMeshProUGUI>().text = " ";
            playerControls.Disable();
        }
        
    }
    void DisableText()
    {
        dialogueText.SetActive(false);
        
        if (Gamepad.current == null)
        {
            this.GetComponent<TextMeshProUGUI>().text = "Press F to interact";
        }
        else
        {
            this.GetComponent<TextMeshProUGUI>().text = "Press Down on the DPad to interact";
        }
    }
    private void OnDisable()
    {
        DisableText();
    }
    private void OnEnable()
    {
        playerControls.Enable();
        
        if (Gamepad.current == null)
        {
            this.GetComponent<TextMeshProUGUI>().text = "Press F to interact";
        }
        else
        {
            this.GetComponent<TextMeshProUGUI>().text = "Press Down on the DPad to interact";
        }
    }

}
