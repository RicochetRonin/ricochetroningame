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

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if on platform
        //if "s" or "down arrow" key pressed
        //disable collider for a certain time? Allowing player to drop down past platform...
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        if(onGround && Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            /*Debug.Log("on ground and pressing down");*/
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }

        
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
