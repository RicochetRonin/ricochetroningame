using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro dialogueText;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            dialogueText.enabled = true;

        }
    }
}
