using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCheck : MonoBehaviour
{
    //Attach to the Player Gameobject, with Rigidbody2D, BoxCollider2D, and Transform
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    
    [HideInInspector] public bool onGround;
    [HideInInspector] public bool onWall;
    [HideInInspector] public bool onRightWall;
    [HideInInspector] public bool onLeftWall;

    private bool wasInAir = false;
    
    [Header("Settings")]
    [SerializeField] private int wallSide;
    [SerializeField] private float collisionRadius = 0.2f;
    [SerializeField] private Vector2 bottomOffset, rightOffset, leftOffset;

    [SerializeField] private AudioClip landingSFX;

    private Color debugCollisionColor = Color.green;

    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        //var collider = Physics2D.OverlapCircle((Vector2) transform.position + bottomOffset, collisionRadius);
        //Debug.Log(collider);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
                 || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        if (!onGround)
        {
            wasInAir = true;
        }
        
        if (onGround && wasInAir) Landing();
    }

    void Landing()
    {
        AudioManager.PlayOneShotSFX(landingSFX);
        wasInAir = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
    
}