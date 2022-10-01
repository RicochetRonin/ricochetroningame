using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DOn't forget input system
using UnityEngine.InputSystem;

public class Cube : MonoBehaviour
{
    private CubeControls _cubeControls;

    [Header("Settings")] 
    [Tooltip("Float representing percentage of change")][SerializeField] private float scaleModifier = 0.1f;
    [SerializeField] private float speed; 
    [Tooltip("Float between 0-360 works best, negative for reverse rotation")][SerializeField] private float rotateSpeed;

    private Vector2 _move;
    private Vector2 _rotate;
    
    #region Initialization
    private void OnEnable()
    {
        _cubeControls.Movement.Enable();
    }

    private void OnDisable()
    {
        _cubeControls.Movement.Disable();
    }
    
    void Awake()
    {
        SetControls();
    }

    void SetControls()
    {
        _cubeControls = new CubeControls();

        //started, performed, cancelled
        //using a lambda expression to ignore context
        _cubeControls.Movement.Grow.performed += _ => Grow();
        
        _cubeControls.Movement.Shrink.performed += _ => Shrink();

        //When we want to set something to the value of a control, and access the values in a function
        _cubeControls.Movement.Move.performed += context => _move = context.ReadValue<Vector2>();
        //No movement occuring
        _cubeControls.Movement.Move.canceled += _ => _move = Vector2.zero;

        _cubeControls.Movement.Rotate.performed += context => _rotate = context.ReadValue<Vector2>();
        _cubeControls.Movement.Rotate.canceled += _ => _rotate = Vector2.zero;
    }
    
    #endregion

    #region Movement Functions

    private Vector2 r, m;

    private void Update()
    {
        //You can have a binding set the values for two different actions
        //While holding Left Shift, the bindings for movement now set the values for rotate
        if (Keyboard.current.leftShiftKey.IsPressed())
        {
            r = new Vector2(_move.y, _move.x) * Time.deltaTime * rotateSpeed;
        }
        else
        {
            //We still want to call the controller bindings
            m = new Vector2(_move.x, _move.y) * Time.deltaTime * speed;
            r = new Vector2(_rotate.y, _rotate.x) * Time.deltaTime * rotateSpeed;
        }
        
        transform.Translate(m, Space.World);
        transform.Rotate(r, Space.World);
        
    }

    private void Grow()
    {
        transform.localScale += (Vector3.one * scaleModifier);
        //Debug.Log(transform.localScale);
    }

    private void Shrink()
    {
        transform.localScale -= (Vector3.one * scaleModifier);
        //Debug.Log(transform.localScale);
    }
    #endregion
 
}
