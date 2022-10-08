using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer enemyGraphics;

    [Header("Stats")]
    [SerializeField] private float health = 3f;
    [SerializeField] private float maxHealth = 3f;
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

        if (canTakeDamage)
        {

            health -= damage;

            enemyGraphics.color = new Color(255f, 0f, 0f, 1f);
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
        enemyGraphics.color = new Color(255f, 255f * colorMultiplier, 255f * colorMultiplier);

    }
}