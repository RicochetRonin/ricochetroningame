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

    private PlayerControls _playerControls;
    private bool _isPaused;

    public GameObject PauseMenu;
    public GameObject FinishedText;

    public static Vector2 lastCheckPointPos;

    #region Singleton

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        _playerControls = new PlayerControls();
        _isPaused = false;

        _playerControls.Pausing.Pause.performed += _ => PauseGame();
        
        if (lastCheckPointPos != null)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
        }
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
    */
    private void OnEnable()
    {
        _playerControls.Pausing.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Pausing.Disable();
    }
    /*
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

    private void Update()
    {

    }

    public void PauseGame()
    {
        if (!_isPaused)
        {
            _isPaused = true;
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
    }

    public void UnPauseGame()
    {
        if (_isPaused)
        {
            _isPaused = false;
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
    }

    public void FinishGame()
    {
        Time.timeScale = 0;
        FinishedText.SetActive(true);
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public PlayerMovement findPlayerMovement()
    {
        return FindObjectOfType<PlayerMovement>();
    }


}
