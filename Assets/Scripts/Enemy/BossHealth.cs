using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer enemyGraphics;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject phase2Teleport;
    [SerializeField] private GameObject phase3Teleport;

    [Header("Stats")]
    [SerializeField] public float health = 30f;
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private float iframes = 0.5f;
    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private float deathDelay = 0.75f;


    private BossShoot bossShoot;
    private GameObject[] enemyBullets;
    private GameObject[] playerBullets;

    private bool isAlive;

    private void Start()
    {
        bossShoot = FindObjectOfType<BossShoot>();
        isAlive = true;
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            isAlive = false;
            ClearBullets();
            StartCoroutine("DeathSequence");
        }
    }

    public void ClearBullets()
    {
        enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

        foreach (GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }

        foreach (GameObject bullet in playerBullets)
        {
            Destroy(bullet);
        }
    }

    private void teleportBoss(int phaseNumber)
    {
        if (phaseNumber == 2)
        {
            enemy.transform.position = phase2Teleport.transform.position;
        }
        else if (phaseNumber == 3)
        {
            enemy.transform.position = phase3Teleport.transform.position;
        }
    }

    public void TakeDamage(float damage)
    {

        if (canTakeDamage)
        {

            health -= damage;

            if (health == 20)
            {
                bossShoot.SetPhaseNumber(2);
                ClearBullets();
                teleportBoss(2);

            }

            else if (health == 10)
            {
                bossShoot.SetPhaseNumber(3);
                ClearBullets();
                teleportBoss(3);
            }

            //Setting enemy graphic to red to indicate damage. Then reset back to original color
            enemyGraphics.color = new Color(255f, 0f, 0f, 1f);
            StartCoroutine("ResetColor");
            canTakeDamage = false;
        }


    }

    private IEnumerator DeathSequence()
    {
        //animator.SetTrigger("Death");
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

    public bool getIsAlive()
    {
        return isAlive;
    }
}