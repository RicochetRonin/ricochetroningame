using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameManager;

public class PlayerAim : MonoBehaviour
{
    private PlayerControls _playerControls;

    private Vector2 _mouseDirection, _gamepadDirection, _mousePosition;
    [HideInInspector] public Vector2 aimDirection;

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
        _playerControls.Aiming.Gamepad.canceled += context => _gamepadDirection = context.ReadValue<Vector2>();

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
            AimMouse();
            aimDirection = _mouseDirection;
            //angle = CalcAimDirection();
            //transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            AimGamepad();
            aimDirection = _gamepadDirection;
            
        }
    }

    void AimGamepad()
    {
        transform.eulerAngles = new Vector3(0f, 0f, -Mathf.Atan2(_gamepadDirection.x, _gamepadDirection.y) * Mathf.Rad2Deg);
    }

    void AimMouse()
    {
        var newDir = (_mouseDirection - new Vector2(Screen.width / 2, Screen.height / 2)).normalized;
        //Vector2 camPos = mainCam.transform.position;
        //var newDir = (_mouseDirection - camPos).normalized;
        
        //transform.rotation = Quaternion.Euler(0, 0, CalcAimDirection2());
        //transform.eulerAngles = new Vector3(0f, 0f, CalcAimDirection2());

        transform.eulerAngles = new Vector3(0f, 0f, -Mathf.Atan2(newDir.x, newDir.y) * Mathf.Rad2Deg + 90);
        //transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(newDir.y, newDir.x) * Mathf.Rad2Deg);
        
        //transform.eulerAngles = new Vector3(0f, 0f, (-Mathf.Atan2(newDir.y, newDir.x) * Mathf.Rad2Deg + 90));
        //Debug.Log(new Vector3(0f, 0f, (-Mathf.Atan2(newDir.x, newDir.y) * Mathf.Rad2Deg) + 90));
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
    
    private float CalcAimDirection()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        aimDirection = (mousePosition - Camera.main.transform.position).normalized;
        Debug.DrawRay(transform.position, aimDirection, Color.blue);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        return angle;
        
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
