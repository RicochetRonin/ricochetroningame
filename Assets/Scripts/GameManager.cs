using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using TMPro;


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
    [SerializeField] private float _offsetY;

    public static Vector2 lastCheckPointPos;
    public static bool checkPointActive;
    public static bool newSceneLoaded = true;

    public static float startTime;
    private float currentTime;

    //Stat Tracking
    public static float damageTaken = 0f;
    public static int enemiesKilled = 0;
    public static int bulletsReflected = 0;
    public static int playerDeaths = 0;
    public Time totalTime;
    public static string timerVal = "";



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

    void Start()
    {
        //Start a new timer
        if (timerVal == "")
        {
            startTime = Time.time;
        }
    }

    void Update()
    {
        float t = Time.time - startTime;
        currentTime = t;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerVal = minutes + ":" + seconds;
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
        Debug.Log("Bullets reflected: " + bulletsReflected);
        Debug.Log("Enemies Killed: " + enemiesKilled);
        Debug.Log("Damage Taken: " + damageTaken);
        Debug.Log("Player deaths: " + playerDeaths);
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

    public void ParamMenuLoadScene(string sceneName)
    {
        UnPauseGame();

        bulletsReflected = 0;
        enemiesKilled = 0;
        damageTaken = 0;
        playerDeaths = 0;
        startTime = Time.time;

        SceneManager.LoadScene(sceneName);
        newSceneLoaded = true;
        checkPointActive = false;
    }

    public void ParamMenuLoadScene(int sceneIndex)
    {
        UnPauseGame();

        bulletsReflected = 0;
        enemiesKilled = 0;
        damageTaken = 0;
        playerDeaths = 0;
        startTime = Time.time;

        SceneManager.LoadScene(sceneIndex);
        newSceneLoaded = true;
        checkPointActive = false;
    }


}
