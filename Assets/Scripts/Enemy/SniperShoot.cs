using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShoot : EnemyShoot
{

    [SerializeField] private AudioClip sniperSFX;
    LaserAim laserAim;

    private GameObject target;

    private void Start()
    {
        laserAim = GetComponentInParent<LaserAim>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        if (!Physics2D.Linecast(transform.position, target.transform.position, 1 << 8))
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
}
