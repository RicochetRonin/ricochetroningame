using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public enum CurrentInput
{
    Mouse,
    Gamepad,
}
public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    /*
    public static CurrentInput CurrentInput;
    void Start()
    {
        PlayerInput input = FindObjectOfType<PlayerInput>();
        Debug.Log(input);
        SetCurrentInput(input.currentControlScheme);
    }

    private void OnEnable()
    {
        InputUser.onChange += onInputDeviceChange;
    }

    private void OnDisable()
    {
        InputUser.onChange -= onInputDeviceChange;
    }

    private void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged) 
        {
            SetCurrentInput(user.controlScheme.Value.name);
            Debug.Log(user.controlScheme.Value.name);
        }
    }

    private void SetCurrentInput(string schemeName)
    {
        Debug.Log(schemeName);
        if (schemeName.Equals("Gamepad"))
        {
            CurrentInput = CurrentInput.Gamepad;
        }
        else
        {
            CurrentInput = CurrentInput.Mouse;
        }
    }
    */
}
