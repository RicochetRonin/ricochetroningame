using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PulseShoot : EnemyShoot
{

    [SerializeField] protected int numProjectiles = 6;
    [SerializeField] protected int numPulse = 3;
    [SerializeField] protected float timeBetweenPulse = 2f;
    [SerializeField] protected float timeBetweenShot = 0f;
    [SerializeField] protected float pulseRadius = 1f;

    private EnemyAim _enemyAim;


    private void Start()
    {
        _enemyAim = this.GetComponentInParent<EnemyAim>();
    }


    void Update()
    {
        if (canAttack)
        {
            StartCoroutine("PulseSpawn");
            canAttack = false;
            StartCoroutine("ResetCoolDown");
        }
    }

   private IEnumerator PulseSpawn()
    {
        //Debug.Log("Pulse called");
        float angleStep = 360 / numProjectiles;
        float currAngle = 0;
        Quaternion newTransformRotation;
        Quaternion currTransformRotation;


        for (int j = 0; j < numPulse; j++) { 
            
            currTransformRotation = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles + new Vector3(0, 0, -90));
            _enemyAim.setCanAim(false);
        
            //Debug.Log("Pulse " + j);
            for (int i = 0; i < numProjectiles; i++)
            {
                
                var offSetx = pulseRadius * Mathf.Cos((currTransformRotation.eulerAngles.z + 90) * Mathf.Deg2Rad);
                var offSety = pulseRadius * Mathf.Sin((currTransformRotation.eulerAngles.z + 90) * Mathf.Deg2Rad);
                var spawnPosition = transform.parent.transform.parent.position;
                spawnPosition.x += offSetx;
                spawnPosition.y += offSety;
                //Debug.Log("Shooting " + i + " at spawnPosition " + spawnPosition + " with Rotation " + currTransformRotation.eulerAngles);
                MasterPool.Spawn(bulletPrefab, spawnPosition, currTransformRotation);
                //Instantiate(bulletPrefab, spawnPosition, currTransformRotation);
                newTransformRotation = Quaternion.Euler(currTransformRotation.eulerAngles + new Vector3(0, 0, angleStep));
                currTransformRotation = newTransformRotation;
                currAngle += angleStep;
                yield return new WaitForSeconds(timeBetweenShot);
            }
            yield return new WaitForSeconds(timeBetweenPulse);
            _enemyAim.setCanAim(true);
        }


        
    }
}
