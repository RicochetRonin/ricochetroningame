using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private bool canDash;
    private bool isDashing;
    private bool isFacingRight;
    private int isFacingRightInt;

    //[SerializeField] private AudioManager audio;
    [SerializeField] private AudioClip jumpSFX, dashSFX, landingSFX;
    
    [Header("Stats")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float slideSpeed = 3f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float wallJumpDistance = 5f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float dashForce = 2f;
    [SerializeField] private float dashTime = 2f;
    [SerializeField] private float dashCoolDown = 2f;
    [SerializeField] private float wallJumpTime;
    private float wallJumpCounter;

    [Header("References")] [SerializeField]
    private PlayerHealth _playerHealth;

    [SerializeField] private PlayerAim _playerAim;
    

    public SpriteRenderer _spriteRenderer;
    public Animator _animator;

    public bool canMove = true;
    private int jumpCount = 0;
    [SerializeField] private LayerMask collisionMask;
    
    [Header("Booleans")]
    public bool wallGrab;
    public bool wallJump;
    private bool wasOnGround;
    private bool wallJumping;

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
        isFacingRight = true;
        isFacingRightInt = 1;
    }

    void SetControls()
    {
        _playerControls = new PlayerControls();

        _playerControls.Moving.Move.performed += context => _move = context.ReadValue<Vector2>();
        _playerControls.Moving.Move.canceled += context => _move = Vector2.zero;

        _playerControls.Moving.Jump.performed += _ => Jump();
        _playerControls.Moving.Dash.performed += _ => StartCoroutine(Dash());
    }


    #endregion

    void Update()
    {
        //Debug.Log(playerHealth.getCanTakeDamage());
        //Debug.Log("Velocity " + rb.velocity);
        //Debug.Log("Y velocity " + rb.velocity.y);
        JumpCheck();
        
        if (!canMove) return;
        
        if (isDashing)
        {
            return;
        }
        
        Vector2 dir = new Vector2(_move.x, _move.y);
        Move(dir);

        if (coll.onGround || coll.onWall)
        {
            jumpCount = 1;
            
            if (!wasOnGround)
            {
                wasOnGround = true;
                //AudioManager.PlayOneShotSFX(landingSFX);
            }
        }
        dashCooldownText.SetCooldown(canDash);
        //Debug.Log("On Wall " + coll.onWall);
        _animator.SetBool("OnWall", (coll.onWall));
        _animator.SetBool("OnGround", (coll.onGround));
        _animator.SetFloat("Speed", Mathf.Abs(dir.x));
        _animator.SetFloat("JumpSpeed", rb.velocity.y);
        _animator.SetBool("FacingRight", isFacingRight);
        _animator.SetBool("WallJumping", wallJumping);
    }

    void SetCanMove()
    {
        canMove = false;
    }

    #region MovementFunctions
    
    private void Move(Vector2 dir)
    {
        //Debug.Log("Wall jumping value " + wallJumping);
        if (dir.x > 0 && !isFacingRight && (coll.onGround))
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = false;
        }

        else if (dir.x < 0 && isFacingRight && (coll.onGround))
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = true;
        }

        else if (coll.onWall && !coll.onGround && !wallJumping)
        {
            if (coll.onRightWall)
            {
                if (isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    isFacingRightInt *= -1;
                    _spriteRenderer.flipX = true;
                }
                
            }
            else
            {
                if (!isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    isFacingRightInt *= -1;
                    _spriteRenderer.flipX = false;
                }                
            }
        
        }

        if (!wallJumping && isFacingRight && dir.x < 0 && !coll.onGround && !coll.onWall)
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = true;
        }

        else if (!wallJumping && !isFacingRight && dir.x > 0 && !coll.onGround && !coll.onWall)
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = false;
        }

        //If we are jumping from a wall, inverse the x direction. Replace rb.velocity.y > 0 with something different for different timings. (Maybe more conditions)
        if (wallJumping && rb.velocity.y > 0 && dir.x != 0){
            rb.velocity = (new Vector2(dir.x * speed*-1, rb.velocity.y));
        }
        else{
            wallJumping = false;
            rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
        }
        //Debug.Log("B " + dir.x * speed);
    }

    private void JumpCheck()
    {
        //increases the gravity on the player's rigidbody as they fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            wasOnGround = false;
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
            if ((coll.onRightWall || coll.onLeftWall) && !coll.onGround)
            {
                _animator.SetTrigger("WallJump");
                wallJumping = true;
                rb.velocity = Vector2.up * jumpVelocity;

                //rb.velocity = ((Vector2.up * jumpVelocity) + (Vector2.left * wallJumpDistance));

                //Debug.LogFormat("Velocity: {0}", rb.velocity);
                //Debug.Log("Wall jump left");

            }
            else if (coll.onLeftWall && !coll.onGround)
            {
                //rb.AddForce((Vector2.up + Vector2.right) * jumpVelocity * wallJumpDistance); //this one

                //rb.velocity = ((Vector2.up * jumpVelocity) + (Vector2.right * wallJumpDistance));


                Debug.LogFormat("Velocity: {0}", rb.velocity);
                Debug.Log("Wall jump right");

            }
            else
            {
                rb.velocity = Vector2.up * jumpVelocity; 
            }
            
            AudioManager.PlayOneShotSFX(jumpSFX);
            jumpCount++;
            //Debug.Log(jumpCount);
        }
    }

    private IEnumerator Dash()
    {
        //Debug.Log("Dash pressed");
        //Debug.Log("isDashing " + isDashing);
        //Debug.Log("canDash " + canDash);
        //Debug.Log("Dash " + canDash);
        //Debug.Log("X" + _move.x);

        if (canDash)
        {
            canDash = false;
            isDashing = true;
            playerHealth.setCanTakeDamage(false);
            //float origGrav = rb.gravityScale;

            rb.gravityScale = 0;
            
            AudioManager.PlayOneShotSFX(dashSFX);

            rb.velocity = new Vector2(isFacingRightInt * dashForce * speed, 0);
            
            yield return new WaitForSeconds(dashTime);
            playerHealth.setCanTakeDamage(true);
            isDashing = false;
            
            rb.gravityScale = 1;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //rb.gravityScale = origGrav;
            
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }

    }

    #endregion

}
