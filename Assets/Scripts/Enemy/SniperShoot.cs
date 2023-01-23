using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShoot : EnemyShoot
{

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
            MasterPool.SpawnBullet(bulletPrefab, transform.position, transform.rotation);
            AudioManager.PlayOneShotSFX(sniperSFX);

            canAttack = false;
            StartCoroutine("ResetCoolDown");
            StartCoroutine(laserAim.AlphaStep(fireRate));
        }

    }
}
