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
    private float playerInputDir;


    [SerializeField] private AudioClip jumpSFX, dashSFX;
    
    [Header("Stats")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float wallJumpVelocity = 5f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float dashForce = 2f;
    [SerializeField] private float dashTime = 2f;
    [SerializeField] private float dashCoolDown = 2f;

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
        playerInputDir = 0;

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

        JumpCheck();
        
        if (!canMove) return;
        
        if (isDashing)
        {
            return;
        }
        
        Vector2 dir = new Vector2(_move.x, _move.y);
        playerInputDir = dir.x;
        Move(dir);

        if (coll.onGround || coll.onWall)
        {
            jumpCount = 1;
            
        }
        dashCooldownText.SetCooldown(canDash);

        //Setting the Ronin animator values
        _animator.SetBool("OnWall", (coll.onWall));
        _animator.SetBool("MovingIntoWall", ((coll.onLeftWall) || (coll.onRightWall)));
        _animator.SetBool("OnGround", (coll.onGround));
        _animator.SetFloat("Speed", Mathf.Abs(playerInputDir));
        _animator.SetFloat("JumpSpeed", rb.velocity.y);
        _animator.SetBool("FacingRight", isFacingRight);
        _animator.SetBool("WallJumping", wallJumping);
    }


    #region MovementFunctions
    
    //Handles movement and sprite flipping to match direction
    private void Move(Vector2 dir)
    {

        //If movement is right and Ronin is facing left and Ronin is on ground, flip Ronin to face right
        if (dir.x > 0 && !isFacingRight && (coll.onGround))
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = false;
        }

        //If movement is left and Ronin is facing right and Ronin is on ground, flip Ronin to face left
        else if (dir.x < 0 && isFacingRight && (coll.onGround))
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = true;
        }

        //If Ronin is on the wall and not on the ground and not wall jumping, flip the sprite depending on which wall Ronin is on.
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

        //If Ronin is not wall jumping and is in the air and is facing right, but player input is left, make the Ronin face left
        if (!wallJumping && isFacingRight && dir.x < 0 && !coll.onGround && !coll.onWall)
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = true;
        }

        //If Ronin is not wall jumping and is in the air and is facing left, but player input is right, make the Ronin face right
        else if (!wallJumping && !isFacingRight && dir.x > 0 && !coll.onGround && !coll.onWall)
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = false;
        }

        //If we are jumping from a wall, inverse the x direction. Replace rb.velocity.y > 0 with something different for different timings
        if (wallJumping && rb.velocity.y > 0 && dir.x != 0){
            rb.velocity = (new Vector2(dir.x * speed*-1, rb.velocity.y));
        }
        else{
            wallJumping = false;
            rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
        }
    }


    //This is called to check that if Ronin is jumped, the gravity is correct as the Ronin goes up and down during his jump
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
    
    //Makes Ronin jump when called
    private void Jump()
    {
        if (jumpCount < maxJumps)
        {
            //If the Ronin is wall clinging, wall jump
            if (((coll.onRightWall && playerInputDir == 1) || (coll.onLeftWall && playerInputDir == -1)) && !coll.onGround)
            {
                wallJumping = true;
                rb.velocity = Vector2.up * wallJumpVelocity;

            }

            else
            {
                rb.velocity = Vector2.up * jumpVelocity; 
            }
            
            AudioManager.PlayOneShotSFX(jumpSFX);
            jumpCount++;
        }
    }

    //Called to make the Ronin dash
    private IEnumerator Dash()
    {
        if (canDash)
        {
            canDash = false;
            isDashing = true;

            //Ronin invicible during dash
            playerHealth.setCanTakeDamage(false);

            //Ronin unaffected by gravity while dashing
            rb.gravityScale = 0;
            
            AudioManager.PlayOneShotSFX(dashSFX);

            rb.velocity = new Vector2(isFacingRightInt * dashForce * speed, 0);
            
            yield return new WaitForSeconds(dashTime);

            //Ronin can take damage after dash
            playerHealth.setCanTakeDamage(true);

            isDashing = false;

            //Ronin affected by gravity again
            rb.gravityScale = 1;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            
            //Dash cooldown
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }

    }

    #endregion

    #region GettersSetters
    void SetCanMove()
    {
        canMove = false;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void setJumpVelocity(float newSpeed)
    {
        jumpVelocity = newSpeed;
    }

    public void setMaxJumps(int newSpeed)
    {
        maxJumps = newSpeed;
    }
    public void setFallMultiplier(float newSpeed)
    {
        fallMultiplier = newSpeed;
    }

    public void setLowJumpMultiplier(float newSpeed)
    {
        lowJumpMultiplier = newSpeed;
    }

    public void setDashForce(float newSpeed)
    {
        dashForce = newSpeed;
    }

    public void setDashTime(float newSpeed)
    {
        dashTime = newSpeed;
    }

    public void setDashCooldown(float newSpeed)
    {
        dashCoolDown = newSpeed;
    }

    #endregion
}
