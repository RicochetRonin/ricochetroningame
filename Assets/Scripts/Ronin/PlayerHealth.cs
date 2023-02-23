﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public Health healthBar; //Attach UI/Health to this slot

    //Attach to the HurtBox Gameobject, child of Player, with BoxCollider2D,and Sprite Renderer
    public delegate void OnDeath();
    public static event OnDeath onDeath;

    private bool hasInvoked;
    
    [Header("Stats")]
    public float health = 20f;
    public float maxHealth = 20f;
    public float iframes = 0.5f;
    public bool canTakeDamage = true;
    [SerializeField] private float deathDelay = 2f;

    [Header("References")] [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerReflect _playerReflect;

    void Awake()
    {
        hasInvoked = false;
    }
    private void Start()
    {
        health = maxHealth;
        _movement._animator.SetFloat("PlayerHealth", (health));
        healthBar.SetPlayerHealth(health, maxHealth);
        canTakeDamage = true;
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

            //Change Ronin color to red, and then reset to normal color
            spriteRenderer.color = new Color(255f, 0f, 0f, 1f);
            StartCoroutine("ResetColor");

            SleepManager.Sleep(10);
            CinemachineShake.Shake(0.3f, 4);


            canTakeDamage = false;
            _movement._animator.SetFloat("PlayerHealth", (health));
            healthBar.SetPlayerHealth(health, maxHealth);
        }


    }

    private IEnumerator DeathSequence()
    {
        canTakeDamage = false;
        _movement.canMove = false;
        _playerReflect.canReflect = false;
        yield return new WaitForSeconds(deathDelay);
        Destroy(player);

        if (hasInvoked == false)
        {
            onDeath?.Invoke();
            hasInvoked = true;
        }
        
        
        /*
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.newSceneLoaded = false;
        Debug.Log("Same scene, does not reset checkpoint");
        */
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillBox"))
        {
            Debug.Log("Death by KillBox");
            StartCoroutine("DeathSequence");
        }
    }

    private IEnumerator ResetColor()
    {

        yield return new WaitForSeconds(iframes);
        canTakeDamage = true;
        float colorMultiplier = health / maxHealth;
        spriteRenderer.color = new Color(255f, 255f * colorMultiplier, 255f * colorMultiplier, 1f);

    }

    public void setCanTakeDamage(bool canTakeDamage)
    {
        this.canTakeDamage = canTakeDamage;
    }

    public bool getCanTakeDamage()
    {
        return this.canTakeDamage;
    }

    public void setMaxHealth(float newMaxHp)
    {
        this.maxHealth = newMaxHp;
        this.health = newMaxHp;
    }
}
