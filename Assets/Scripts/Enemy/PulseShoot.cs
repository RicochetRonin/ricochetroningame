using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PulseShoot : EnemyShoot
{

    [SerializeField] protected int numProjectiles = 6;
    [SerializeField] protected int numPulse = 3;
    [SerializeField] protected float timeBetweenPulse = 2f;
    [SerializeField] protected float timeBetweenShot = 0f;
    [SerializeField] protected float pulseRadius = 1f;

    [SerializeField] protected AudioClip PulseSFX;
    private bool origCanAim;
    private EnemyAim _enemyAim;

    private GameObject target;


    private void Start()
    {
        _enemyAim = this.GetComponentInParent<EnemyAim>();
        origCanAim = _enemyAim.getCanAim();
        target = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (!Physics2D.Linecast(transform.position, target.transform.position, 1 << 8))
        {
            if (setAttackStarted == false)
            {
                StartCoroutine("SetCanAttack");
                setAttackStarted = true;
            }

            if (canAttack)
            {
                StartCoroutine("PulseSpawn");
                canAttack = false;
                StartCoroutine("ResetCoolDown");
            }
        }
    }

   private IEnumerator PulseSpawn()
    {
        float angleStep = 360 / numProjectiles;
        float currAngle = 0;
        Quaternion newTransformRotation;
        Quaternion currTransformRotation;

        for (int j = 0; j < numPulse; j++) { 
            
            currTransformRotation = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles + new Vector3(0, 0, -90));

            //Locking enemy aim during firing sequence
            if (origCanAim) { _enemyAim.setCanAim(false); }
            bodyAnimator.SetTrigger("Shoot");

            for (int i = 0; i < numProjectiles; i++)
            {
                
                var offSetx = pulseRadius * Mathf.Cos((currTransformRotation.eulerAngles.z + 90) * Mathf.Deg2Rad);
                var offSety = pulseRadius * Mathf.Sin((currTransformRotation.eulerAngles.z + 90) * Mathf.Deg2Rad);
                var spawnPosition = transform.parent.transform.parent.position;
                spawnPosition.x += offSetx;
                spawnPosition.y += offSety;
                MasterPool.SpawnBullet(bulletPrefab, spawnPosition, currTransformRotation);

                AudioManager.PlayOneShotSFX(PulseSFX);

                newTransformRotation = Quaternion.Euler(currTransformRotation.eulerAngles + new Vector3(0, 0, angleStep));
                currTransformRotation = newTransformRotation;
                currAngle += angleStep;
                yield return new WaitForSeconds(timeBetweenShot);
            }
            yield return new WaitForSeconds(timeBetweenPulse);
            if (origCanAim) { _enemyAim.setCanAim(true); }
        }


        
    }
}
