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

    void OnEnable()
    {
        StartCoroutine("SetCanAttack");
    }

    IEnumerator SetCanAttack()
    {
        yield return new WaitForSeconds(firstShotDelay);
        canAttack = true;

    }
    void Update()
    {
        if (canAttack)
        {
            bulletPrefab.GetComponent<BulletController>().SetEnemyTag();
            MasterPool.Spawn(bulletPrefab, transform.position, transform.rotation);
            
            canAttack = false;
            StartCoroutine("ResetCoolDown");
        }
    }

    IEnumerator ResetCoolDown()
    {
        yield return new WaitForSeconds(fireRate);
        canAttack = true;
    }
}
