using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : EnemyShoot
{
    
    [Header("Shotgun Settings")]
    [SerializeField] protected float bulletSpread = 15;



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
                Quaternion newTransformRotation;
                Quaternion currTransformRotation;

                currTransformRotation = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles + new Vector3(0, 0, -90 - bulletSpread));


                for (int i = 0; i < 3; i++)
                {

                    var offSetx = Mathf.Cos((currTransformRotation.eulerAngles.z + 90) * Mathf.Deg2Rad);
                    var offSety = Mathf.Sin((currTransformRotation.eulerAngles.z + 90) * Mathf.Deg2Rad);
                    var spawnPosition = transform.parent.transform.parent.position;
                    spawnPosition.x += offSetx;
                    spawnPosition.y += offSety;
                    MasterPool.SpawnBullet(bulletPrefab, spawnPosition, currTransformRotation);

                    newTransformRotation = Quaternion.Euler(currTransformRotation.eulerAngles + new Vector3(0, 0, bulletSpread));
                    currTransformRotation = newTransformRotation;
                }

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

