using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private bool canAttack = true;

    // Update is called once per frame
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
