using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    
    [Header("Settings")]
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private bool canAttack = true;
    
    void Update()
    {
        if (canAttack)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
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
