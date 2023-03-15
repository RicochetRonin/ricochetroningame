using System;
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
    [SerializeField] protected AudioClip ShootSFX;
    [SerializeField] protected Animator bodyAnimator;
    [SerializeField] protected Animator armAnimator;
    protected GameObject target;

    protected float defaultFireRate;


    [SerializeField] protected EnemyHealth enemyHealth;
    public bool setAttackStarted = false;


    private void Start()
    {
        defaultFireRate = fireRate;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        canAttack = false;
        setAttackStarted = false;
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
            if (setAttackStarted == false)
            {
                StartCoroutine("SetCanAttack");
                setAttackStarted = true;
            }

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

    public virtual void setMultipliedFireRate(float fireRateMultiplier)
    {
        fireRate = defaultFireRate * (1/ fireRateMultiplier);
    }

    public virtual void setDefaultFireRate()
    {
        fireRate = defaultFireRate;
    }
}