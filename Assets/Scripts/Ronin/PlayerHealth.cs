using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public Health healthBar; //Attach UI/Health to this slot

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
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerReflect _playerReflect;
        
    private void Start()
    {
        health = maxHealth;
        healthBar.SetPlayerHealth(health, maxHealth);
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
            healthBar.SetPlayerHealth(health, maxHealth);
        }


    }

    private IEnumerator DeathSequence()
    {
        canTakeDamage = false;
        //Debug.Log("Dead");
        onDeath?.Invoke();
        _movement.canMove = false;
        _playerReflect.canReflect = false;
        //Sound Effect
        //Particle effect
        //Start Animation
        yield return new WaitForSeconds(deathDelay);
        Destroy(player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillBox"))
        {
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
}
