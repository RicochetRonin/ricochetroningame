using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameManager;

public class PlayerAim : MonoBehaviour
{
    private PlayerControls _playerControls;

    private Vector2 _mouseDirection, _gamepadDirection, _mousePosition;
    [HideInInspector] public Vector2 aimDirection, newDir;

    public bool usingController;

    #region Initialization

    private void OnEnable()
    {
        _playerControls.Aiming.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Aiming.Disable();
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();

        _playerControls.Aiming.Gamepad.performed += context => _gamepadDirection = context.ReadValue<Vector2>();
        //_playerControls.Aiming.Gamepad.canceled += context => _gamepadDirection = context.ReadValue<Vector2>();

        _playerControls.Aiming.Mouse.performed += context => _mouseDirection = context.ReadValue<Vector2>();
        _playerControls.Aiming.Mouse.canceled += context => _mouseDirection = context.ReadValue<Vector2>();
        
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion

    private float angle;
    
    private void Update()
    {
        if (Gamepad.current == null)
        {
            usingController = false;
            AimMouse();
            aimDirection = _mouseDirection;
            //angle = CalcAimDirection();
            //transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            usingController = true;
            AimGamepad();
            aimDirection = _gamepadDirection;
            
        }

    }

    void AimGamepad()
    {
        Debug.Log(_gamepadDirection);
        newDir = _gamepadDirection;
        
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(_gamepadDirection.y, _gamepadDirection.x) * Mathf.Rad2Deg);
    }

    void AimMouse()
    {
        //newDir = (_mouseDirection - new Vector2(Screen.width / 2, Screen.height / 2)).normalized;
        Vector3 playerCameraPos = Camera.main.WorldToScreenPoint(transform.position);
        
        newDir = (_mouseDirection - new Vector2(playerCameraPos.x, playerCameraPos.y)).normalized;

        transform.eulerAngles = new Vector3(0f, 0f, -Mathf.Atan2(newDir.x, newDir.y) * Mathf.Rad2Deg + 90);
    }

    [SerializeField] private Camera mainCam;
    private float CalcAimDirection2()
    {
        Vector2 camPos = mainCam.transform.position;
        aimDirection = (_mouseDirection - camPos).normalized;
        Debug.DrawRay(transform.position, aimDirection, Color.blue);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        return angle;
    }
}
