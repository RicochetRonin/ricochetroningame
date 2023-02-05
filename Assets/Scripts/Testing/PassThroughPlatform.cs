using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PassThroughPlatform : MonoBehaviour
{
    // CITE: PitiIT YT video -> https://youtu.be/Lyeb7c0-R8c

    private Collider2D _collider;
    [HideInInspector] public bool onGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float collisionRadius = 0.2f;
    [SerializeField] private Vector2 bottomOffset;
    private Vector2 _move;
    private PlayerControls _playerControls;


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Awake();
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();

        _playerControls.Moving.Move.performed += context => _move = context.ReadValue<Vector2>();
        _playerControls.Moving.Move.canceled += context => _move = Vector2.zero;

        _playerControls.Moving.Move.performed += ctx => dropDown();
    }

    private void OnEnable()
    {
        _playerControls.Moving.Enable();

        // _playerHealth.onDeath += SetCanMove;
    }

    private void OnDisable()
    {
        _playerControls.Moving.Disable();

        // _playerHealth.onDeath -= SetCanMove;
    }

    private void dropDown()
    {
        /*Debug.Log("on ground and pressing down");*/
        _collider.enabled = false;
        StartCoroutine(EnableCollider());
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<PlayerMovement>(); // Grab any component that exists on the player.
        if (player != null)
        {
            onGround = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, false);
    }
}
