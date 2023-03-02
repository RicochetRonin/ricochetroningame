using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer enemyGraphics;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject reflectHitbox;
    [SerializeField] private GameObject bossInteractable;

    [Header("Move/Teleport Locations")]
    [SerializeField] private GameObject phase2Teleport;
    [SerializeField] private GameObject phase3Teleport;
    [SerializeField] private List<GameObject> offScreenPositions;

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
    private bool movedOffScreen1, movedOffScreen2 = false;
    private bool moving = false;

    private void Start()
    {
        bossShoot = FindObjectOfType<BossShoot>();
        isAlive = true;
        health = maxHealth;
    }

    private void Update()
    {
        var step = 4.0f * Time.deltaTime;

        if (moving == true)
        {
            enemy.GetComponentInChildren<BossShoot>().enabled = false;
        }
        else
        {
            enemy.GetComponentInChildren<BossShoot>().enabled = true;
        }

        if (health == 20 && movedOffScreen1 == false)
        {
            ClearBullets();

            moving = true;

            player.GetComponent<PlayerMovement>().canMove = false;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, offScreenPositions[0].transform.position, step);
            

            if (enemy.transform.position == offScreenPositions[0].transform.position)
            {
                moving = false;
                
                movedOffScreen1 = true;
                teleportBoss(2);
                bossShoot.SetPhaseNumber(2);
                player.GetComponent<PlayerMovement>().canMove = true;
                enemy.GetComponentInChildren<LaserAim>().enabled = true;
                enemy.GetComponentInChildren<LineRenderer>().enabled = true;
                enemy.GetComponentInChildren<SniperShoot>().enabled = true;
            }
        }

        if (health == 10 && movedOffScreen2 == false)
        {
            ClearBullets();

            moving = true;

            player.GetComponent<PlayerMovement>().canMove = false;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, offScreenPositions[1].transform.position, step);


            if (enemy.transform.position == offScreenPositions[1].transform.position)
            {
                moving = false;

                movedOffScreen2 = true;
                teleportBoss(3);
                bossShoot.SetPhaseNumber(3);
                player.GetComponent<PlayerMovement>().canMove = true;
                reflectHitbox.SetActive(true);
            }
        }

        if (health <= 0)
        {
            isAlive = false;
            reflectHitbox.SetActive(false);
            ClearBullets();
            StartCoroutine("DeathSequence");
            bossInteractable.SetActive(true);
        }
    }

    public void ClearBullets()
    {
        enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

        foreach (GameObject bullet in enemyBullets)
        {
            MasterPool.DespawnBullet(bullet);
        }

        foreach (GameObject bullet in playerBullets)
        {
            MasterPool.DespawnBullet(bullet);
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