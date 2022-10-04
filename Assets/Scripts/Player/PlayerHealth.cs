﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    //Attach to the HurtBox Gameobject, child of Player, with BoxCollider2D,and Sprite Renderer
    [Header("Private Components")]

    [Header("Stats")]
    public float health = 20f;
    public float maxHealth = 20f;
    public float iframes = 0.5f;
    public bool canTakeDamage = true;

    [Header("References")] [SerializeField]
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        
        health = maxHealth;

    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        
        if (canTakeDamage)
        {

            health -= damage;

            spriteRenderer.color = new Color(255f, 0f, 0f, 1f);
            StartCoroutine("ResetColor");
            canTakeDamage = false;
        }
        

    }

    private void Die()
    {

        //Debug.Log("Dead");

    }

    private IEnumerator ResetColor()
    {

        yield return new WaitForSeconds(iframes);
        canTakeDamage = true;
        float colorMultiplier = health / maxHealth;
        spriteRenderer.color = new Color(255f, 255f * colorMultiplier, 255f * colorMultiplier, 1f);

    }
}
