using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer enemyGraphics;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Animator animator;

    [Header("Stats")]
    [SerializeField] private float health = 3f;
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float iframes = 0.5f;
    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private float deathDelay = 0.75f;

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

            //Setting enemy graphic to red to indicate damage. Then reset back to original color
            enemyGraphics.color = new Color(255f, 0f, 0f, 1f);
            StartCoroutine("ResetColor");
            canTakeDamage = false;
        }


    }

    private IEnumerator DeathSequence()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(deathDelay);
        Destroy(enemy);

    }

    private IEnumerator ResetColor()
    {

        yield return new WaitForSeconds(iframes);
        canTakeDamage = true;
        float colorMultiplier = health / maxHealth;
        enemyGraphics.color = new Color(255f, 255f * colorMultiplier, 255f * colorMultiplier);

    }
}