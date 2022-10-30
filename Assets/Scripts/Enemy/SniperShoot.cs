using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShoot : EnemyShoot
{


    [SerializeField] public Color defaultColor;
    [SerializeField] public Color chargeColor;

    [SerializeField] public ColorController colorController;

    void Update()
    {
        if (canAttack)
        {   
            //Instantiate(bulletPrefab, transform.position, transform.rotation);
            
            bulletPrefab.GetComponent<BulletController>().SetEnemyTag();
            MasterPool.Spawn(bulletPrefab, transform.position, transform.rotation);

            canAttack = false;
            StartCoroutine("ResetCoolDown");
            StartCoroutine(colorController.FadeColor(defaultColor, chargeColor, fireRate));
        }

    }
}
