using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [Header("Private Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Stats")]
    [SerializeField] private float health = 20f;
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private float iframes = 0.5f;
    [SerializeField] private bool canTakeDamage = true;

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

        if (canTakeDamage == true)
        {

            health -= damage;

            spriteRenderer.color = new Color(255f, 0f, 0f, 1f);
            StartCoroutine("ResetColor");
            canTakeDamage = false;
        }


    }

    private void Die()
    {

        Debug.Log("Dead");

    }

    private IEnumerator ResetColor()
    {

        yield return new WaitForSeconds(iframes);
        canTakeDamage = true;
        float colorMultiplier = health / maxHealth;
        spriteRenderer.color = new Color(255f, 255f * colorMultiplier, 255f * colorMultiplier, 1f);

    }
}
