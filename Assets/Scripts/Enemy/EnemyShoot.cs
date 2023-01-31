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
    [SerializeField] private Animator animator;

    private EnemyHealth enemyHealth;


    private void Start()
    {
        enemyHealth =  this.transform.parent.transform.parent.GetComponentInChildren<EnemyHealth>();
        Debug.Log(enemyHealth);
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
        if (canAttack && enemyHealth.getIsAlive())
        {
            Debug.Log("Can Attack! " + canAttack + " isAlive " + enemyHealth.getIsAlive());
            canAttack = false;
            MasterPool.SpawnBullet(bulletPrefab, transform.position, transform.rotation);
            AudioManager.PlayOneShotSFX(ShootSFX);
            animator.SetTrigger("Shoot");
            StartCoroutine("ResetCoolDown");
        }
    }

    IEnumerator ResetCoolDown()
    {
        yield return new WaitForSeconds(fireRate);
        canAttack = true;
    }
}
