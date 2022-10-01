using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameManager;

public class Aim : MonoBehaviour
{
    private PlayerControls _playerControls;

    private Vector2 _mouseDirection, _gamepadDirection;

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
        _playerControls.Aiming.Gamepad.canceled += context => _gamepadDirection = Vector2.zero;

        _playerControls.Aiming.Mouse.performed += context => _mouseDirection = context.ReadValue<Vector2>();
        _playerControls.Aiming.Mouse.canceled += context => _mouseDirection = Vector2.zero;
    }

    #endregion

    private void Update()
    {
        if (Gamepad.current == null)
        {
            AimMouse();
        }
        else
        {
            AimGamepad();
        }
    }

    void AimGamepad()
    {
        transform.eulerAngles = new Vector3(0f, 0f, -Mathf.Atan2(_gamepadDirection.x, _gamepadDirection.y) * Mathf.Rad2Deg);
    }

    void AimMouse()
    {
        var newDir = (_mouseDirection - new Vector2(Screen.width / 2, Screen.height / 2)).normalized;
        transform.eulerAngles = new Vector3(0f, 0f, (-Mathf.Atan2(newDir.x, newDir.y) * Mathf.Rad2Deg) + 90);
    }
}
