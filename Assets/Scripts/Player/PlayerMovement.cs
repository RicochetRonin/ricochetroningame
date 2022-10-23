using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Code from Celeste's Movement by Mix and Jam: https://www.youtube.com/watch?v=STyY26a_dPY&ab_channel=MixandJam
    //Code from Better Jumping in Unity With Four Lines of Code by Board To Bits Games: https://www.youtube.com/watch?v=7KiK0Aqtmzc&ab_channel=BoardToBitsGames

    public DashCooldown dashCooldownText; //Attach UI/DashCooldown to this slot

    //Attach to the Player Gameobject, with Rigidbody2D, BoxCollider2D, and Transform
    [Header("Private Components")]
    private PlayerWallCheck coll;
    private Rigidbody2D rb;
    private PlayerControls _playerControls;
    private Vector2 _move;
    private PlayerHealth playerHealth;
    private UnityEngine.InputSystem.InputAction.CallbackContext _dash;
    private float dashWait;
    private bool canDash;
    private bool isDashing;


    [Header("Stats")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float slideSpeed = 3f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float dashForce = 2f;
    [SerializeField] private float dashTime = 2f;
    [SerializeField] private float dashCoolDown = 2f;

    [Header("References")] [SerializeField]
    private PlayerHealth _playerHealth;

    private bool canMove = true;
    private int jumpCount = 0;
    
    [Header("Booleans")]
    public bool wallGrab;
    public bool wallSlide;

    #region Initialization

    private void OnEnable()
    {
        _playerControls.Moving.Enable();

        _playerHealth.onDeath += SetCanMove;
    }

    private void OnDisable()
    {
        _playerControls.Moving.Disable();
        
        _playerHealth.onDeath -= SetCanMove;
    }

    private void Awake()
    {
        SetControls();
        
        coll = GetComponent<PlayerWallCheck>();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponentInChildren<PlayerHealth>();
        canDash = true;
    }

    void SetControls()
    {
        _playerControls = new PlayerControls();

        _playerControls.Moving.Move.performed += context => _move = context.ReadValue<Vector2>();
        _playerControls.Moving.Move.canceled += context => _move = Vector2.zero;

        _playerControls.Moving.Jump.performed += _ => Jump();
        _playerControls.Moving.Jump.performed += _ => WallGrab();
        _playerControls.Moving.Dash.performed += _ => StartCoroutine(Dash());

    }


    #endregion

    void Update()
    {


        //Debug.Log(playerHealth.getCanTakeDamage());

        if (!canMove) return;


        if (isDashing)
        {
            return;
        }
        Vector2 dir = new Vector2(_move.x, _move.y);
        Move(dir);
        //WallGrab();
        JumpCheck();
        
        if (coll.onGround || coll.onWall)
        {
            jumpCount = 1;
        }

        dashCooldownText.SetCooldown(canDash);
    }

    void SetCanMove()
    {
        canMove = false;
    }

    #region MovementFunctions
    
    private void Move(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void JumpCheck()
    {
        //increases the gravity on the player's rigidbody as they fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        
        else if (rb.velocity.y > 0 && !_playerControls.Moving.Jump.triggered)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    
    private void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            jumpCount++;
            //Debug.Log(jumpCount);
        }
    }


    private void WallGrab()
    {
        if (coll.onWall && _playerControls.Moving.WallGrab.triggered)
        {
            wallGrab = true;
            //wallSlide = false;
        }

        //gravityScale is not set back after wall grabbing
        if (wallGrab)
        {
            rb.gravityScale = 0;
            if (_move.x > .2f || _move.x < -.2f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            float speedModifier = _move.y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, _move.y * (speed * speedModifier));
        }

        /*
        if (coll.onWall && !coll.onGround)
        {
            if (_move.x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
        }
        */
    }

    private void WallSlide()
    {
        var velocity = rb.velocity;
        bool pushingWall = (velocity.x > 0 && coll.onRightWall) || (velocity.x < 0 && coll.onLeftWall);
        float push = pushingWall ? 0 : velocity.x;

        velocity = new Vector2(push, -slideSpeed);
        rb.velocity = velocity;
    }

    private IEnumerator Dash()
    {
        //Debug.Log("Dash pressed");
        //Debug.Log("isDashing " + isDashing);
        //Debug.Log("canDash " + canDash);
        //Debug.Log("Dash " + canDash);
        //Debug.Log("X" + _move.x);

        if (canDash && _move.x != 0)
        {
            canDash = false;
            isDashing = true;
            playerHealth.setCanTakeDamage(false);
            float origGrav = rb.gravityScale;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(_move.x * dashForce * speed, 0);
            yield return new WaitForSeconds(dashTime);
            playerHealth.setCanTakeDamage(true);
            isDashing = false;
            rb.gravityScale = origGrav;
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }

    }

    #endregion

}
