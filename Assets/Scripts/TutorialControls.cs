using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialControls : MonoBehaviour
{
    [SerializeField] private GameObject mouseInstructions, gamepadInstructions;

    void Update()
    {
        //Debug.Log(Gamepad.current);
        if (Gamepad.current == null)
        {
            mouseInstructions.SetActive(true);
            gamepadInstructions.SetActive(false);
        }
        else
        {
            gamepadInstructions.SetActive(true);
            mouseInstructions.SetActive(false);
        }
    }
}
