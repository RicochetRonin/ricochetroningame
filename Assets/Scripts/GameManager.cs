using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

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
    [SerializeField] private float _offsetY;

    public static Transform lastCheckPointPos;

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

        /*lastCheckPointPos = GameObject.FindGameObjectWithTag("Player").transform;*/
/*        Debug.Log("This is current checkpoint pos on awake");
        Debug.Log(lastCheckPointPos);

        if (lastCheckPointPos != null)
        {
            Debug.Log("Spawning at checkpoint");
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2 (lastCheckPointPos.position.x, lastCheckPointPos.position.y + _offsetY);
            Debug.LogFormat("Last Checkpoint: {0}", lastCheckPointPos.gameObject.name);
        }
        else
        {
            lastCheckPointPos.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }*/
    }

    #endregion

    void ResetRespawnPosition(Scene scene, LoadSceneMode mode)
    {
        lastCheckPointPos = null;
    }
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
        SceneManager.sceneLoaded += ResetRespawnPosition;
    }

    private void OnDisable()
    {
        _playerControls.Pausing.Disable();
        SceneManager.sceneLoaded -= ResetRespawnPosition;
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

    public void Restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
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

    public PlayerHealth findPlayerHealth()
    {
        return FindObjectOfType<PlayerHealth>();
    }



}
