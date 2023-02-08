using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject bulletPrefab;

    [Header("Settings")]
    [SerializeField] protected float fireRate = 3f;
    [SerializeField] protected bool canAttack = false;
    [SerializeField] protected float firstShotDelay = 2f;
    [SerializeField] private AudioClip ShootSFX;
    [SerializeField] private Animator bodyAnimator;
    [SerializeField] private Animator armAnimator;
    private GameObject target;


    [SerializeField] private EnemyHealth enemyHealth;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        canAttack = false;
        StartCoroutine("SetCanAttack");
    }

    IEnumerator SetCanAttack()
    {
        yield return new WaitForSeconds(firstShotDelay);
        canAttack = true;

    }

    private void Update()
    {
        //Check for any walls between the enemey and the Ronin
        //If no walls are found, allow the enemy to attack
        if (!Physics2D.Linecast(transform.position, target.transform.position, 1 << 8))
        {
            if (canAttack && enemyHealth.getIsAlive())
            {
                canAttack = false;
                MasterPool.SpawnBullet(bulletPrefab, transform.position, transform.rotation);
                AudioManager.PlayOneShotSFX(ShootSFX);
                bodyAnimator.SetTrigger("Shoot");
                armAnimator.SetTrigger("Shoot");
                StartCoroutine("ResetCoolDown");
            }
        }
    }

    IEnumerator ResetCoolDown()
    {
        yield return new WaitForSeconds(fireRate);
        canAttack = true;
    }
}