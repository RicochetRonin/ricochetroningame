﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    //Attach to the HurtBox Gameobject, child of Player, with BoxCollider2D,and Sprite Renderer
    public delegate void OnDeath();
    public event OnDeath onDeath;

    [Header("Stats")]
    public float health = 20f;
    public float maxHealth = 20f;
    public float iframes = 0.5f;
    public bool canTakeDamage = true;
    [SerializeField] private float deathDelay = 2f;

    [Header("References")] [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject player;
    
    private void Start()
    {
        
        health = maxHealth;

    }

    private void Update()
    {
        if (health <= 0)
        {
            StartCoroutine("DeathSequence");
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

    private IEnumerator DeathSequence()
    {
        canTakeDamage = false;
        Debug.Log("Dead");
        onDeath?.Invoke();
        //Sound Effect
        //Particle effect
        //Start Animation
        yield return new WaitForSeconds(deathDelay);
        Destroy(player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ResetColor()
    {

        yield return new WaitForSeconds(iframes);
        canTakeDamage = true;
        float colorMultiplier = health / maxHealth;
        spriteRenderer.color = new Color(255f, 255f * colorMultiplier, 255f * colorMultiplier, 1f);

    }
}
