using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShoot : EnemyShoot
{

    //No longer needed
    //[SerializeField] public Color defaultColor;
    //[SerializeField] public Color chargeColor;

    //[SerializeField] public ColorController colorController;


    [SerializeField] public ColorController colorController;
    [SerializeField] private AudioClip sniperSFX;
    LaserAim laserAim;

    private void Start()
    {
        laserAim = GetComponentInParent<LaserAim>();
    }

    
    void Update()
    {
        if (canAttack)
        {
            //Instantiate(bulletPrefab, transform.position, transform.rotation);

            //bulletPrefab.GetComponent<BulletController>().SetHostile();
            MasterPool.Spawn(bulletPrefab, transform.position, transform.rotation);
            AudioManager.PlayOneShotSFX(sniperSFX);

            canAttack = false;
            StartCoroutine("ResetCoolDown");
            //StartCoroutine(colorController.FadeColor(defaultColor, chargeColor, fireRate));
            StartCoroutine(laserAim.AlphaStep(fireRate));
        }

    }
}
