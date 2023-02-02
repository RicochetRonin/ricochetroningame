using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject bulletPrefab;

    [Header("Settings")]
    [SerializeField] protected float fireRate = 3f;
    [SerializeField] protected float bulletSpread = 15;
    [SerializeField] protected bool canAttack = false;
    [SerializeField] protected float firstShotDelay = 2f;
    [SerializeField] private AudioClip ShootSFX;
    [SerializeField] private Animator animator;

    [SerializeField] private EnemyHealth enemyHealth;

    private float shot1Angle;
    private float shot2Angle;
    private float shot3Angle;

    private Quaternion shot1Quaternion;
    private Quaternion shot2Quaternion;
    private Quaternion shot3Quaternion;

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
            canAttack = false;
            Quaternion newTransformRotation;
            Quaternion currTransformRotation;

            currTransformRotation = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles + new Vector3(0, 0, -90-bulletSpread));


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






            //canAttack = false;
            //MasterPool.SpawnBullet(bulletPrefab, this.transform.position, this.transform.rotation);
            //MasterPool.SpawnBullet(bulletPrefab, this.transform.position, Quaternion.Euler(new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z + bulletSpread)));
            //MasterPool.SpawnBullet(bulletPrefab, this.transform.position, Quaternion.Euler(new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z - bulletSpread)));
            AudioManager.PlayOneShotSFX(ShootSFX);
            //animator.SetTrigger("Shoot");
            StartCoroutine("ResetCoolDown");
        }
    }

    IEnumerator ResetCoolDown()
    {
        yield return new WaitForSeconds(fireRate);
        canAttack = true;
    }
}

