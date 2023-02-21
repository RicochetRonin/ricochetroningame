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
    private int previousScene;

    public GameObject PauseMenu;
    public GameObject FinishedText;
    [SerializeField] private float _offsetY;

    public static Vector2 lastCheckPointPos;
    public static bool checkPointActive;
    public static bool newSceneLoaded;

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
        

        //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2 (lastCheckPointPos.position.x, lastCheckPointPos.position.y + _offsetY);

        //Debug.Log("Spawning at checkpoint");

        //Debug.LogFormat("Last Checkpoint: {0}", lastCheckPointPos.gameObject.name);
    }

    #endregion

    /*
    void SetPreviousScene(Scene old, Scene current)
    {
        //previousScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.LogFormat("Previous scene is now: {0}", previousScene);

        Debug.LogFormat("Comparing last scene, {0} to new scene, {1}", old.buildIndex, current.buildIndex);
        if (old != current)
        {
            newSceneLoaded = true;
            Debug.Log("New scene, reset checkpoint");
            checkPointActive = false;
        }
        else
        {
            newSceneLoaded = false;
            Debug.Log("Same scene, does not reset checkpoint");
        }
    }
    
    void ResetRespawnPosition(Scene scene, LoadSceneMode mode)
    {
        
        
        if (SceneManager.GetActiveScene().buildIndex != previousScene)
        {
            
            
        }
        else
        {
            
            
        }
        
    }
    */

    /*
    void MovePlayerOnSpawn()
    {
        //Debug.Log("Moving Player");
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(lastCheckPointPos.x, lastCheckPointPos.y + _offsetY);
    }
    */
    
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
        //SceneManager.sceneLoaded += ResetRespawnPosition;
        //SceneManager.activeSceneChanged += SetPreviousScene;
        //PlayerMovement.SpawnSet += MovePlayerOnSpawn;

        PlayerHealth.onDeath += Restart;
        PlayerOutOfView.outOfView += Restart;
    }

    private void OnDisable()
    {
        _playerControls.Pausing.Disable();
        //SceneManager.sceneLoaded -= ResetRespawnPosition;
        //SceneManager.activeSceneChanged -= SetPreviousScene;
        //PlayerMovement.SpawnSet -= MovePlayerOnSpawn;

        PlayerHealth.onDeath -= Restart;
        PlayerOutOfView.outOfView -= Restart;

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

    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        newSceneLoaded = false;
        Debug.Log("Same scene, does not reset checkpoint");

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
