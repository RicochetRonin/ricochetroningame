using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float wallJumpDirection;
    private float initalWallJumpX;
    private bool wallJumpInputSwitch;
    private float currentTime;
    private bool jumpStarted;


    //[SerializeField] private AudioClip jumpSFX, dashSFX;

    [Header("Stats")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float wallJumpHorizontalSpeed = 10f;
    [SerializeField] private float wallJumpVerticalSpeed = 10f;
    [SerializeField] private float wallSlideGravityReducer = 3;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private int maxDashes = 1;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float dashForce = 2f;
    [SerializeField] private float dashTime = 2f;
    [SerializeField] private float dashCoolDown = 2f;
    [SerializeField] private float coyoteTime = 0.25f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [Header("References")] [SerializeField]
    private PlayerHealth _playerHealth;
    private DashCooldown dashCooldownText;

    [SerializeField] private PlayerAim _playerAim;
    

    public SpriteRenderer _spriteRenderer;
    public Animator _animator;
    [SerializeField] private RoninSoundManager soundManager;

    public bool canMove = true;
    private int dashCount;
    private int jumpCount = 0;
    [SerializeField] private LayerMask collisionMask;
    
    [Header("Booleans")]
    private bool wallGrab;
    private bool wallJump;
    private bool wallJumping;
    private bool wallSliding;
    private bool prevWallSliding;
    
    public delegate void SpawnPointEventHandler();
    public static event SpawnPointEventHandler SpawnSet;

    private float coyoteTimeCounter;
    private bool jumpBuffer;

    #region Initialization

    private void OnEnable()
    {
        _playerControls.Moving.Enable();

        PlayerHealth.onDeath += SetCanMove;
        
        SceneManager.sceneLoaded += Spawn;
    }

    private void OnDisable()
    {
        _playerControls.Moving.Disable();
        
        PlayerHealth.onDeath -= SetCanMove;
        
        SceneManager.sceneLoaded -= Spawn;
    }

    private void Awake()
    {
        SetControls();
        
        coll = GetComponent<PlayerWallCheck>();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponentInChildren<PlayerHealth>();
        dashCooldownText = GameObject.FindObjectOfType<DashCooldown>();
        canDash = true;
        isFacingRight = true;
        isFacingRightInt = 1;
        playerInputDir = 0;
        wallSliding = false;
        prevWallSliding = false;
        jumpBuffer = false;
        jumpStarted = false;
    }

    void SetControls()
    {
        _playerControls = new PlayerControls();

        _playerControls.Moving.Move.performed += context => _move = context.ReadValue<Vector2>();
        _playerControls.Moving.Move.canceled += context => _move = Vector2.zero;

        //_playerControls.Moving.Jump.performed += _ => Jump();
        _playerControls.Moving.Dash.performed += _ => StartCoroutine(Dash());
    }


    #endregion

    void Spawn(Scene scene, LoadSceneMode mode)
    {
        transform.position = new Vector2(GameManager.lastCheckPointPos.x, GameManager.lastCheckPointPos.y);
        if (SpawnSet != null) SpawnSet();
        
        /*
        if (GameManager.checkPointActive == false)
        {
            //Debug.Log("Set To Player Start Position");
            GameManager.lastCheckPointPos = new Vector2(transform.position.x, transform.position.y);
            if (SpawnSet != null) SpawnSet();
        }
        else
        {
            //Debug.Log("Spawn Player at last checkpoint");
            transform.position = new Vector2(GameManager.lastCheckPointPos.x, GameManager.lastCheckPointPos.y);
            if (SpawnSet != null) SpawnSet();
        }
        */
    }
    void Update()
    {
        //Debug.Log("Velocity " + rb.velocity);
        if (!canMove) return;
        
        if (isDashing)
        {
            return;
        }
        
        Vector2 dir = new Vector2(_move.x, _move.y);

        if (dir.x > 0.01) { playerInputDir = 1; }
        else if (dir.x < -0.01) { playerInputDir = -1; }
        else { playerInputDir = 0; }
        
        Move(dir);

        WallSlideCheck();
        Jump();
        JumpCheck();
        WallJumpingCheck();

/*        Debug.Log(jumpCount);*/
        if ((coll.onGround || coll.onPlatform || coll.onWall) && !jumpBuffer)
        {
            /*            Debug.Log("resetting coyote time");*/
            coyoteTimeCounter = coyoteTime;
            jumpCount = 0;
            dashCount = 0;

        } else
        {
/*            Debug.Log("coyote time counting down");*/
            coyoteTimeCounter -= Time.deltaTime;
        }
        dashCooldownText.SetCooldown(canDash);

        //Setting the Ronin animator values
        _animator.SetBool("OnWall", (coll.onWall));
        _animator.SetBool("MovingIntoWall", ((coll.onLeftWall && _move.x < 0) || (coll.onRightWall && _move.x > 0)));
        _animator.SetBool("OnGround", (coll.onGround));
        _animator.SetBool("OnPlatform", (coll.onPlatform));
        _animator.SetBool("OnGroundOrOnPlatform", (coll.onGround || coll.onPlatform));
        _animator.SetFloat("Speed", Mathf.Abs(playerInputDir));
        _animator.SetFloat("JumpSpeed", rb.velocity.y);
        _animator.SetBool("FacingRight", isFacingRight);
        _animator.SetBool("WallJumping", wallJumping);
        _animator.SetBool("WallSliding", wallSliding);
        _animator.SetBool("WallGrab", wallSliding || ((coll.onLeftWall && _move.x < 0) || (coll.onRightWall && _move.x > 0)));
    }


    #region MovementFunctions
    
    //Handles movement and sprite flipping to match direction
    private void Move(Vector2 dir)
    {
        //Debug.Log("Moved called");

        //If movement is right and Ronin is facing left and Ronin is on ground or platform, flip Ronin to face right
        if (dir.x > 0 && !isFacingRight && (coll.onGround || coll.onPlatform))
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = false;
                  }

        //If movement is left and Ronin is facing right and Ronin is on ground or platform, flip Ronin to face left
        else if (dir.x < 0 && isFacingRight && (coll.onGround || coll.onPlatform))
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = true;
        }

        //If Ronin is on the wall and not on the ground and not on platform and not wall jumping, flip the sprite depending on which wall Ronin is on.
        else if (coll.onWall && (!coll.onGround && !coll.onPlatform) && !wallJumping)
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
        if (!wallJumping && isFacingRight && dir.x < 0 && (!coll.onGround && !coll.onPlatform) && !coll.onWall)
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = true;
        }

        //If Ronin is not wall jumping and is in the air and is facing left, but player input is right, make the Ronin face right
        else if (!wallJumping && !isFacingRight && dir.x > 0 && (!coll.onGround && !coll.onPlatform) && !coll.onWall)
        {
            isFacingRight = !isFacingRight;
            isFacingRightInt *= -1;
            _spriteRenderer.flipX = false;
        }

        //Handles walljumping when players presses movement keys mid air
        if (wallJumping && (initalWallJumpX == playerInputDir || playerInputDir == 0 || !wallJumpInputSwitch) && ! wallSliding)
        {
            rb.gravityScale = 1;
            if (!wallJumpInputSwitch)
            {
                if(initalWallJumpX != playerInputDir)
                {
                    wallJumpInputSwitch = true;
                }
            }
        }

        //Wall clinging
        else if ((coll.onLeftWall && dir.x < 0) || (coll.onRightWall && dir.x > 0))
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            
        }

        else if (wallSliding)
        {
            rb.gravityScale = 1;
        }

        else
        {
            rb.gravityScale = 1;
            rb.velocity = (new Vector2(playerInputDir * speed, rb.velocity.y));
            if (playerInputDir != 0 && coll.onGround) { soundManager.Footstep(); }
        }
    }


    //This is called to check that if Ronin is jumped, the gravity is correct as the Ronin goes up and down during his jump
    private void JumpCheck()
    {
        //Debug.Log("Jump check called");
        //increases the gravity on the player's rigidbody as they fall
        if (rb.velocity.y < 0 && !wallSliding)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        
        else if (rb.velocity.y > 0 && !_playerControls.Moving.Jump.triggered || wallJumping)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    
    //Makes Ronin jump when called
    private void Jump()
    {
        currentTime += Time.deltaTime;

        bool isJumpKeyHeld = _playerControls.Moving.Jump.ReadValue<float>() > 0.1f;

        if (isJumpKeyHeld)
        {
            if (jumpCount < maxJumps && jumpStarted == false)
            {
                jumpStarted = true;
                //If the Ronin is wall clinging, wall jump
                if (wallSliding || (coll.onWall && (!coll.onGround && !coll.onPlatform)))
                {
                    wallJumping = true;
                    if (coll.onRightWall)
                    {
                        wallJumpDirection = -1;
                    }

                    else { wallJumpDirection = 1; }

                    rb.velocity = new Vector2(wallJumpDirection * wallJumpHorizontalSpeed, wallJumpVerticalSpeed);
                    initalWallJumpX = playerInputDir;
                    wallJumpInputSwitch = false;
                }
                else if (coyoteTimeCounter < 0f)
                {
                    StartCoroutine(JumpBuffer());
                    rb.velocity = Vector2.up * jumpVelocity;
                    jumpCount++;
                }
                else
                {
                    StartCoroutine(JumpBuffer());
                    rb.velocity = Vector2.up * jumpVelocity;
                }

                //AudioManager.PlayOneShotSFX(jumpSFX);
                soundManager.Jump();
                jumpCount++;
                currentTime = 0;
            }
        }
        else
        {
            if (jumpStarted) 
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2.5f);
                jumpStarted = false;
            }
        }

        /*if (jumpCount < maxJumps)
        {
            //If the Ronin is wall clinging, wall jump
            if (wallSliding || (coll.onWall && (!coll.onGround && !coll.onPlatform)))
            {
                wallJumping = true;
                if (coll.onRightWall)
                {
                    wallJumpDirection = -1;
                }

                else { wallJumpDirection = 1; }

                rb.velocity = new Vector2(wallJumpDirection * wallJumpHorizontalSpeed, wallJumpVerticalSpeed);
                initalWallJumpX = playerInputDir;
                wallJumpInputSwitch = false;
            }
            else if (coyoteTimeCounter < 0f)
            {
                StartCoroutine(JumpBuffer());
                rb.velocity = Vector2.up * jumpVelocity;
                jumpCount++;
            } 
            else
            {
                StartCoroutine(JumpBuffer());
                rb.velocity = Vector2.up * jumpVelocity;
            }

            //AudioManager.PlayOneShotSFX(jumpSFX);
            soundManager.Jump();
            jumpCount++;
        }*/
    }

    public IEnumerator JumpBuffer()
    {
        jumpBuffer = true;
        yield return new WaitForSeconds(jumpBufferTime);
        jumpBuffer = false;
    }

    private void WallSlideCheck()
    {
        //Debug.Log("Wall slide check called");
        if (coll.onWall && (!coll.onGround && !coll.onPlatform) && playerInputDir == 0 && rb.velocity.y < 5)
        {
            //Sets the intial wall sliding velocity
            if (!prevWallSliding)
            {
                rb.velocity = new Vector2(0, 0.1f);
                prevWallSliding = true;
                wallSliding = true;
            }

            //Reduce velocity using wallSlideGravityReducer
            else if (rb.velocity.y > -7 && _move.y != -1)
            {
                wallSliding = true;
                rb.velocity += Vector2.up * Physics2D.gravity.y * (1/wallSlideGravityReducer) * Time.deltaTime;
            }
 
        }

        else
        {
            wallSliding = false;
            prevWallSliding = false;
        }
    }

    private void WallJumpingCheck()
    {
        //Debug.Log("Wall Jumping check called");
        if (wallJumping)
        {
            if (coll.onGround){
                wallJumping = false;
            }
            else if (coll.onWall)
            {
                if ((wallJumpDirection == 1 && coll.onRightWall) || (wallJumpDirection == -1 && coll.onLeftWall) || wallJumpInputSwitch)
                {
                    wallJumping = false;
                }

            }

            else if (wallJumpInputSwitch && playerInputDir != 0)
            {
                wallJumping = false;
            }
        }

    }

    //Called to make the Ronin dash
    private IEnumerator Dash()
    {
        if (canDash && dashCount < maxDashes)
        {
            Vector2 prevVelocity = rb.velocity;
            canDash = false;
            isDashing = true;

            //Ronin invicible during dash
            playerHealth.setCanTakeDamage(false);

            //Ronin unaffected by gravity while dashing
            rb.gravityScale = 0;

            //AudioManager.PlayOneShotSFX(dashSFX);
            soundManager.Dash();

            rb.velocity = new Vector2(isFacingRightInt * dashForce * speed, 0);
            dashCount++;
            
            yield return new WaitForSeconds(dashTime);

            //Ronin can take damage after dash
            playerHealth.setCanTakeDamage(true);

            isDashing = false;
            wallJumping = false;

            //Ronin affected by gravity again
            rb.gravityScale = 1;
            rb.velocity = Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            dashCooldownText.SetCooldown(false);

            //Dash cooldown
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
            dashCooldownText.SetCooldown(true);
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
