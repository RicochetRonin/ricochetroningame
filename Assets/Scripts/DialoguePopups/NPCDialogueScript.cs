using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCDialogueScript : MonoBehaviour
{
    public bool dialogueFinished = false;
    public PlayerControls playerControls;

    [SerializeField] private string[] lines;
    private int linesIndex = 0;

    private TextMeshProUGUI lineText;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Interaction.ProgressDialogue.performed += _ => Talk();
    }
    void Start()
    {
        lineText = gameObject.GetComponent<TextMeshProUGUI>();
        lineText.text = lines[linesIndex];
        dialogueFinished = false;
    }

    void Talk()
    {
        if (linesIndex >= lines.Length - 1)
        {
            linesIndex = 0;
            dialogueFinished = true;
        }
        else
        {
            linesIndex++;
            lineText.text = lines[linesIndex];
        }
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
}
